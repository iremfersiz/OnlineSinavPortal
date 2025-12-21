using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSinavPortal.Models;
using OnlineSinavPortal.ViewModels;

namespace OnlineSinavPortal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "AdminDashboard");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new AdminLoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.KullaniciAdi);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.KullaniciAdi);
            }

            if (user == null || !user.Aktif)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                return View(model);
            }

            // Öğrenci numarası kontrolü
            if (!string.IsNullOrEmpty(model.OgrenciNumarasi))
            {
                if (user.OgrenciNumarasi != model.OgrenciNumarasi)
                {
                    ModelState.AddModelError("", "Öğrenci numarası hatalı!");
                    return View(model);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                model.Sifre,
                model.BeniHatirla,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                // Admin ise AdminDashboard'a, değilse Home'a yönlendir
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            return View(new AdminRegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullanıcı adı kontrolü
            var existingUser = await _userManager.FindByNameAsync(model.KullaniciAdi);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten kullanılıyor!");
                return View(model);
            }

            // E-posta kontrolü
            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kullanılıyor!");
                return View(model);
            }

            // Yeni kullanıcı oluştur
            var user = new ApplicationUser
            {
                UserName = model.KullaniciAdi,
                Email = model.Email,
                EmailConfirmed = true,
                AdSoyad = model.AdSoyad,
                OgrenciNumarasi = model.OgrenciNumarasi,
                Aktif = true,
                OlusturmaTarihi = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Sifre);
            if (result.Succeeded)
            {
                // Varsayılan olarak User rolü ekle
                await _userManager.AddToRoleAsync(user, "User");

                // Otomatik giriş yap
                await _signInManager.SignInAsync(user, isPersistent: false);

                TempData["Success"] = "Kayıt başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

