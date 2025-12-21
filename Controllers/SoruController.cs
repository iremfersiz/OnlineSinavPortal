using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineSinavPortal.Data;
using OnlineSinavPortal.Models;
using OnlineSinavPortal.Repositories;
using OnlineSinavPortal.ViewModels;

namespace OnlineSinavPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SoruController : Controller
    {
        private readonly ISoruRepository _soruRepository;
        private readonly ISinavRepository _sinavRepository;
        private readonly ApplicationDbContext _context;

        public SoruController(ISoruRepository soruRepository, ISinavRepository sinavRepository, ApplicationDbContext context)
        {
            _soruRepository = soruRepository;
            _sinavRepository = sinavRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(int sinavId)
        {
            var sinav = await _sinavRepository.GetByIdAsync(sinavId);
            if (sinav == null)
            {
                return NotFound();
            }

            ViewBag.SinavId = sinavId;
            ViewBag.SinavBaslik = sinav.Baslik;

            var sorular = await _soruRepository.GetSorularBySinavIdAsync(sinavId);
            var soruList = sorular.Select(s => new SoruViewModel
            {
                Id = s.Id,
                SoruMetni = s.SoruMetni,
                Puan = s.Puan,
                Sira = s.Sira,
                SinavId = s.SinavId,
                Secenekler = s.Secenekler.Select(sec => new SecenekViewModel
                {
                    Id = sec.Id,
                    SecenekMetni = sec.SecenekMetni,
                    DogruCevap = sec.DogruCevap,
                    Sira = sec.Sira,
                    SoruId = sec.SoruId
                }).OrderBy(sec => sec.Sira).ToList()
            }).OrderBy(s => s.Sira).ToList();

            return View(soruList);
        }

        public async Task<IActionResult> Create(int sinavId)
        {
            var sinav = await _sinavRepository.GetByIdAsync(sinavId);
            if (sinav == null)
            {
                return NotFound();
            }

            var model = new SoruViewModel
            {
                SinavId = sinavId,
                SinavBaslik = sinav.Baslik,
                Secenekler = new List<SecenekViewModel>
                {
                    new SecenekViewModel { Sira = 1 },
                    new SecenekViewModel { Sira = 2 },
                    new SecenekViewModel { Sira = 3 },
                    new SecenekViewModel { Sira = 4 }
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SoruViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var sinav = await _sinavRepository.GetByIdAsync(model.SinavId);
                if (sinav != null)
                {
                    model.SinavBaslik = sinav.Baslik;
                }
                return View(model);
            }

            if (!model.Secenekler.Any(s => s.DogruCevap))
            {
                ModelState.AddModelError("", "En az bir doğru cevap seçmelisiniz!");
                var sinav = await _sinavRepository.GetByIdAsync(model.SinavId);
                if (sinav != null)
                {
                    model.SinavBaslik = sinav.Baslik;
                }
                return View(model);
            }

            var soru = new Soru
            {
                SoruMetni = model.SoruMetni,
                Puan = model.Puan,
                Sira = model.Sira,
                SinavId = model.SinavId,
                OlusturmaTarihi = DateTime.Now
            };

            await _soruRepository.AddAsync(soru);

            var secenekler = model.Secenekler
                .Where(s => !string.IsNullOrWhiteSpace(s.SecenekMetni))
                .Select(s => new Secenek
                {
                    SecenekMetni = s.SecenekMetni,
                    DogruCevap = s.DogruCevap,
                    Sira = s.Sira,
                    SoruId = soru.Id
                }).ToList();

            if (secenekler.Any())
            {
                _context.Secenekler.AddRange(secenekler);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Soru başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index), new { sinavId = model.SinavId });
        }

        // AJAX ile soru detaylarını getirme
        [HttpGet]
        public async Task<IActionResult> GetSoruDetails(int id)
        {
            var soru = await _soruRepository.GetSoruWithSeceneklerAsync(id);
            if (soru == null)
            {
                return Json(new { success = false, message = "Soru bulunamadı." });
            }

            var model = new
            {
                success = true,
                soru = new
                {
                    id = soru.Id,
                    soruMetni = soru.SoruMetni,
                    puan = soru.Puan,
                    sira = soru.Sira,
                    sinavId = soru.SinavId,
                    secenekler = soru.Secenekler.OrderBy(s => s.Sira).Select(sec => new
                    {
                        id = sec.Id,
                        secenekMetni = sec.SecenekMetni,
                        dogruCevap = sec.DogruCevap,
                        sira = sec.Sira
                    }).ToList()
                }
            };

            return Json(model);
        }

        // AJAX ile soru silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var soru = await _soruRepository.GetSoruWithSeceneklerAsync(id);
            if (soru == null)
            {
                return Json(new { success = false, message = "Soru bulunamadı." });
            }

            var sinavId = soru.SinavId;

            await _soruRepository.DeleteAsync(soru);
            return Json(new { success = true, message = "Soru başarıyla silindi.", sinavId = sinavId });
        }
    }
}






