using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineSinavPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // Artık AccountController'da Logout ve AccessDenied var
        // Bu controller'ı kaldırabiliriz veya eski route'lar için redirect ekleyebiliriz
    }
}
