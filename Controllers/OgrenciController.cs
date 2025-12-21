using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Data;
using OnlineSinavPortal.Models;
using OnlineSinavPortal.Repositories;
using System.Text.Json;

namespace OnlineSinavPortal.Controllers
{
    [Authorize(Roles = "User")]
    public class OgrenciController : Controller
    {
        private readonly ISinavRepository _sinavRepository;
        private readonly ApplicationDbContext _context;

        public OgrenciController(ISinavRepository sinavRepository, ApplicationDbContext context)
        {
            _sinavRepository = sinavRepository;
            _context = context;
        }

        // Kullanılabilir sınavlar listesi
        public async Task<IActionResult> Index()
        {
            var sinavlar = await _sinavRepository.GetAktifSinavlarAsync();
            return View(sinavlar);
        }

        // Sınav detayı ve başlatma
        public async Task<IActionResult> SinavDetay(int id)
        {
            var sinav = await _sinavRepository.GetSinavWithSorularAsync(id);
            if (sinav == null || !sinav.Aktif)
            {
                return NotFound();
            }

            // Sınav zamanı kontrolü
            if (sinav.BaslangicTarihi.HasValue && sinav.BaslangicTarihi.Value > DateTime.Now)
            {
                TempData["Error"] = "Bu sınav henüz başlamamıştır.";
                return RedirectToAction(nameof(Index));
            }

            if (sinav.BitisTarihi.HasValue && sinav.BitisTarihi.Value < DateTime.Now)
            {
                TempData["Error"] = "Bu sınavın süresi dolmuştur.";
                return RedirectToAction(nameof(Index));
            }

            // Kullanıcının daha önce bu sınavı almış mı kontrol et
            var userId = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            
            if (user != null)
            {
                var oncekiSonuc = await _context.SinavSonuclari
                    .FirstOrDefaultAsync(s => s.SinavId == id && s.UserId == user.Id);
                
                if (oncekiSonuc != null)
                {
                    ViewBag.OncekiSonuc = oncekiSonuc;
                }
            }

            return View(sinav);
        }

        // Sınav alma sayfası
        [HttpGet]
        public async Task<IActionResult> SinavaGir(int id)
        {
            var sinav = await _sinavRepository.GetSinavWithSorularAsync(id);
            if (sinav == null || !sinav.Aktif)
            {
                return NotFound();
            }

            // Sınav zamanı kontrolü
            if (sinav.BaslangicTarihi.HasValue && sinav.BaslangicTarihi.Value > DateTime.Now)
            {
                TempData["Error"] = "Bu sınav henüz başlamamıştır.";
                return RedirectToAction(nameof(Index));
            }

            if (sinav.BitisTarihi.HasValue && sinav.BitisTarihi.Value < DateTime.Now)
            {
                TempData["Error"] = "Bu sınavın süresi dolmuştur.";
                return RedirectToAction(nameof(Index));
            }

            // Kullanıcının daha önce bu sınavı almış mı kontrol et
            var userId = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            
            if (user != null)
            {
                var oncekiSonuc = await _context.SinavSonuclari
                    .FirstOrDefaultAsync(s => s.SinavId == id && s.UserId == user.Id);
                
                if (oncekiSonuc != null)
                {
                    TempData["Error"] = "Bu sınavı daha önce aldınız.";
                    return RedirectToAction(nameof(Sonuc), new { sinavSonucId = oncekiSonuc.Id });
                }
            }

            ViewBag.BaslangicZamani = DateTime.Now;
            ViewBag.SureDakika = sinav.Sure;
            return View(sinav);
        }

        // Sınav sonucu gönderme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SinavSonucuGonder(int sinavId, Dictionary<int, int> cevaplar, DateTime baslangicZamani)
        {
            var sinav = await _sinavRepository.GetSinavWithSorularAsync(sinavId);
            if (sinav == null)
            {
                return NotFound();
            }

            var userId = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            if (user == null)
            {
                return Unauthorized();
            }

            // Daha önce bu sınav alınmış mı kontrol et
            var oncekiSonuc = await _context.SinavSonuclari
                .FirstOrDefaultAsync(s => s.SinavId == sinavId && s.UserId == user.Id);
            
            if (oncekiSonuc != null)
            {
                return RedirectToAction(nameof(Sonuc), new { sinavSonucId = oncekiSonuc.Id });
            }

            // Cevapları değerlendir
            int dogruSayisi = 0;
            int yanlisSayisi = 0;
            int bosSayisi = 0;
            int alinanPuan = 0;

            foreach (var soru in sinav.Sorular.OrderBy(s => s.Sira))
            {
                if (cevaplar.ContainsKey(soru.Id))
                {
                    var secilenSecenekId = cevaplar[soru.Id];
                    var secilenSecenek = soru.Secenekler.FirstOrDefault(s => s.Id == secilenSecenekId);
                    
                    if (secilenSecenek != null)
                    {
                        if (secilenSecenek.DogruCevap)
                        {
                            dogruSayisi++;
                            alinanPuan += soru.Puan;
                        }
                        else
                        {
                            yanlisSayisi++;
                        }
                    }
                }
                else
                {
                    bosSayisi++;
                }
            }

            var bitisZamani = DateTime.Now;
            var sureDakika = (int)(bitisZamani - baslangicZamani).TotalMinutes;
            var gectiMi = alinanPuan >= sinav.GecmeNotu;

            // Sınav sonucunu kaydet
            var sinavSonucu = new SinavSonucu
            {
                SinavId = sinavId,
                UserId = user.Id,
                AlinanPuan = alinanPuan,
                ToplamPuan = sinav.ToplamPuan,
                DogruSayisi = dogruSayisi,
                YanlisSayisi = yanlisSayisi,
                BosSayisi = bosSayisi,
                BaslangicZamani = baslangicZamani,
                BitisZamani = bitisZamani,
                SureDakika = sureDakika,
                GectiMi = gectiMi,
                OlusturmaTarihi = DateTime.Now,
                CevaplarJson = JsonSerializer.Serialize(cevaplar)
            };

            _context.SinavSonuclari.Add(sinavSonucu);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Sonuc), new { sinavSonucId = sinavSonucu.Id });
        }

        // Sınav sonucu görüntüleme
        public async Task<IActionResult> Sonuc(int sinavSonucId)
        {
            var sinavSonucu = await _context.SinavSonuclari
                .Include(s => s.Sinav)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == sinavSonucId);

            if (sinavSonucu == null)
            {
                return NotFound();
            }

            // Kullanıcı kendi sonucunu mu görüntülüyor kontrol et
            var userId = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            
            if (user != null && sinavSonucu.UserId != user.Id)
            {
                return Forbid();
            }

            var sinav = await _sinavRepository.GetSinavWithSorularAsync(sinavSonucu.SinavId);
            ViewBag.Sinav = sinav;

            return View(sinavSonucu);
        }

        // Kullanıcının sınav sonuçları listesi
        public async Task<IActionResult> Sonuclarim()
        {
            var userId = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            
            if (user == null)
            {
                return Unauthorized();
            }

            var sonuclar = await _context.SinavSonuclari
                .Include(s => s.Sinav)
                .Where(s => s.UserId == user.Id)
                .OrderByDescending(s => s.OlusturmaTarihi)
                .ToListAsync();

            return View(sonuclar);
        }
    }
}

