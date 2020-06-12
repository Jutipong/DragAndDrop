using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TCRB.DAL.Model.Appsetting;
using TCRB.DAL.Model.Authentication;
using TCRB.DAL.Model.Commons;
using TCRB.DAL.Model.UserLogin;

namespace TCRB.WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppsittingModel _appsitting;

        public LoginController(IOptions<AppsittingModel> appsitting)
        {
            _appsitting = appsitting.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {

            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return (IActionResult)View();

        }

        [HttpPost]
        //public async Task<JsonResult> IndexAsync(UserProfileModel model)
        public async Task<JsonResult> IndexAsync(UserLoginModel model)
        {
            ResponseModel result = new ResponseModel();
            //var obj = (ClaimResponseModel)result.data;
            //var identity = new ClaimsIdentity(new[] {
            //        new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            //        new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(model))
            //});
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
            //{
            //    ExpiresUtc = DateTime.UtcNow.AddMinutes(2),
            //});

            var UserProfile = new UserProfileModel
            {
                UserID = "T621506",
                UserName = "Jutipong.Su",
                Name = "Jutipong",
                Last = "Subin",
                Role = "Developer"
            };

            var claims = new List<Claim> {
                new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(UserProfile)),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Developer")
            };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(_appsitting.LoginTimeExpired)
            });

            result.Success = true;
            result.Datas = @Url.Action("Index", "Configuration");
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Index", "Login");
        }

    }
}
