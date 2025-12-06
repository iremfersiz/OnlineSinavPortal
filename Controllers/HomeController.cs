using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortal.Models;
using OnlineSinavPortal.Repositories;
using OnlineSinavPortal.ViewModels;
using System.Security.Claims;

namespace OnlineSinavPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdminRepository _adminRepository;

    public HomeController(ILogger<HomeController> logger, IAdminRepository adminRepository)
    {
        _logger = logger;
        _adminRepository = adminRepository;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "AdminDashboard");
        }
        return View(new AdminLoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AdminLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var isValid = await _adminRepository.ValidateLoginAsync(model.KullaniciAdi, model.Sifre, model.OgrenciNumarasi);
        if (!isValid)
        {
            ModelState.AddModelError("", "Kullanıcı adı, öğrenci numarası veya şifre hatalı!");
            return View(model);
        }

        var admin = await _adminRepository.GetByKullaniciAdiAsync(model.KullaniciAdi);
        if (admin == null || !admin.Aktif)
        {
            ModelState.AddModelError("", "Hesabınız aktif değil!");
            return View(model);
        }

        // Cookie oluşturma
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Name, admin.KullaniciAdi),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim("AdSoyad", admin.AdSoyad ?? admin.KullaniciAdi)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.BeniHatirla,
            ExpiresUtc = model.BeniHatirla ? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddHours(1)
        };

        await HttpContext.SignInAsync(
            "CookieAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "AdminDashboard");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "AdminDashboard");
        }
        return View(new AdminRegisterViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AdminRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Kullanıcı adı kontrolü
        var existingAdmin = await _adminRepository.GetByKullaniciAdiAsync(model.KullaniciAdi);
        if (existingAdmin != null)
        {
            ModelState.AddModelError("", "Bu kullanıcı adı zaten kullanılıyor!");
            return View(model);
        }

        // E-posta kontrolü
        var existingEmail = await _adminRepository.GetByEmailAsync(model.Email);
        if (existingEmail != null)
        {
            ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor!");
            return View(model);
        }

        // Öğrenci numarası kontrolü
        var existingOgrenciNo = await _adminRepository.GetByOgrenciNumarasiAsync(model.OgrenciNumarasi);
        if (existingOgrenciNo != null)
        {
            ModelState.AddModelError("", "Bu öğrenci numarası zaten kayıtlı!");
            return View(model);
        }

        // Yeni admin oluştur
        var admin = new Admin
        {
            KullaniciAdi = model.KullaniciAdi,
            Email = model.Email,
            AdSoyad = model.AdSoyad,
            OgrenciNumarasi = model.OgrenciNumarasi,
            Sifre = HashPassword(model.Sifre),
            Aktif = true,
            OlusturmaTarihi = DateTime.Now
        };

        await _adminRepository.AddAsync(admin);

        TempData["Success"] = "Kayıt başarıyla oluşturuldu! Giriş yapabilirsiniz.";
        return RedirectToAction("Index");
    }

    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
