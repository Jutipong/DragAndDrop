using Microsoft.AspNetCore.Mvc;

namespace TCRB.WEB.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
