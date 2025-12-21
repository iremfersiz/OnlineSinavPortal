using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortal.Repositories;

namespace OnlineSinavPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly ISinavRepository _sinavRepository;

        public AdminDashboardController(ISinavRepository sinavRepository)
        {
            _sinavRepository = sinavRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Sorular'ı da yüklemek için Include kullanıyoruz
            var sinavlar = await _sinavRepository.GetAllWithSorularAsync();
            ViewBag.ToplamSinav = sinavlar.Count();
            ViewBag.AktifSinav = sinavlar.Count(s => s.Aktif);
            ViewBag.ToplamSoru = sinavlar.Sum(s => s.Sorular?.Count ?? 0);
            ViewBag.SonSinavlar = sinavlar.OrderByDescending(s => s.OlusturmaTarihi).Take(5).ToList();
            
            return View();
        }
    }
}
