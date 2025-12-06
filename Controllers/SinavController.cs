using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortal.Models;
using OnlineSinavPortal.Repositories;
using OnlineSinavPortal.ViewModels;
using System.Security.Claims;

namespace OnlineSinavPortal.Controllers
{
    [Authorize]
    public class SinavController : Controller
    {
        private readonly ISinavRepository _sinavRepository;
        private readonly IAdminRepository _adminRepository;

        public SinavController(ISinavRepository sinavRepository, IAdminRepository adminRepository)
        {
            _sinavRepository = sinavRepository;
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sinavlar = await _sinavRepository.GetAllAsync();
            var sinavList = sinavlar.Select(s => new SinavViewModel
            {
                Id = s.Id,
                Baslik = s.Baslik,
                Aciklama = s.Aciklama,
                Sure = s.Sure,
                ToplamPuan = s.ToplamPuan,
                GecmeNotu = s.GecmeNotu,
                Aktif = s.Aktif,
                BaslangicTarihi = s.BaslangicTarihi,
                BitisTarihi = s.BitisTarihi,
                OlusturmaTarihi = s.OlusturmaTarihi,
                SoruSayisi = s.Sorular?.Count ?? 0
            }).ToList();

            return View(sinavList);
        }

        public IActionResult Create()
        {
            return View(new SinavViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinavViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var sinav = new Sinav
            {
                Baslik = model.Baslik,
                Aciklama = model.Aciklama,
                Sure = model.Sure,
                ToplamPuan = model.ToplamPuan,
                GecmeNotu = model.GecmeNotu,
                Aktif = model.Aktif,
                BaslangicTarihi = model.BaslangicTarihi,
                BitisTarihi = model.BitisTarihi,
                AdminId = adminId,
                OlusturmaTarihi = DateTime.Now
            };

            await _sinavRepository.AddAsync(sinav);
            TempData["Success"] = "Sınav başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sinav = await _sinavRepository.GetByIdAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            var model = new SinavViewModel
            {
                Id = sinav.Id,
                Baslik = sinav.Baslik,
                Aciklama = sinav.Aciklama,
                Sure = sinav.Sure,
                ToplamPuan = sinav.ToplamPuan,
                GecmeNotu = sinav.GecmeNotu,
                Aktif = sinav.Aktif,
                BaslangicTarihi = sinav.BaslangicTarihi,
                BitisTarihi = sinav.BitisTarihi,
                OlusturmaTarihi = sinav.OlusturmaTarihi,
                SoruSayisi = sinav.Sorular?.Count ?? 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SinavViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var sinav = await _sinavRepository.GetByIdAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            sinav.Baslik = model.Baslik;
            sinav.Aciklama = model.Aciklama;
            sinav.Sure = model.Sure;
            sinav.ToplamPuan = model.ToplamPuan;
            sinav.GecmeNotu = model.GecmeNotu;
            sinav.Aktif = model.Aktif;
            sinav.BaslangicTarihi = model.BaslangicTarihi;
            sinav.BitisTarihi = model.BitisTarihi;

            await _sinavRepository.UpdateAsync(sinav);
            TempData["Success"] = "Sınav başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sinav = await _sinavRepository.GetSinavWithSorularAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            var model = new SinavViewModel
            {
                Id = sinav.Id,
                Baslik = sinav.Baslik,
                Aciklama = sinav.Aciklama,
                Sure = sinav.Sure,
                ToplamPuan = sinav.ToplamPuan,
                GecmeNotu = sinav.GecmeNotu,
                Aktif = sinav.Aktif,
                BaslangicTarihi = sinav.BaslangicTarihi,
                BitisTarihi = sinav.BitisTarihi,
                OlusturmaTarihi = sinav.OlusturmaTarihi,
                SoruSayisi = sinav.Sorular?.Count ?? 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinav = await _sinavRepository.GetByIdAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            await _sinavRepository.DeleteAsync(sinav);
            TempData["Success"] = "Sınav başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var sinav = await _sinavRepository.GetSinavWithSorularAsync(id);
            if (sinav == null)
            {
                return NotFound();
            }

            var model = new SinavViewModel
            {
                Id = sinav.Id,
                Baslik = sinav.Baslik,
                Aciklama = sinav.Aciklama,
                Sure = sinav.Sure,
                ToplamPuan = sinav.ToplamPuan,
                GecmeNotu = sinav.GecmeNotu,
                Aktif = sinav.Aktif,
                BaslangicTarihi = sinav.BaslangicTarihi,
                BitisTarihi = sinav.BitisTarihi,
                OlusturmaTarihi = sinav.OlusturmaTarihi,
                SoruSayisi = sinav.Sorular?.Count ?? 0
            };

            return View(model);
        }
    }
}


