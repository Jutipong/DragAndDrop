using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TCRB.WEB.Helpers
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            var binding = context.HttpContext.Features.Get<ITlsTokenBindingFeature>()?.GetProvidedTokenBindingId();
            var tlsTokenBinding = binding == null ? null : Convert.ToBase64String(binding);
            var cookie = context.Options.CookieManager.GetRequestCookie(context.HttpContext, context.Options.Cookie.Name);
            if (cookie != null)
            {
                var ticket = context.Options.TicketDataFormat.Unprotect(cookie, tlsTokenBinding);
                var expiresUtc = ticket.Properties.ExpiresUtc;
                var currentUtc = DateTime.UtcNow;
                if (expiresUtc != null && expiresUtc.Value < currentUtc)
                {
                    context.RedirectUri = new PathString("/Login/Logout");
                    //var url = new PathString("/Account/SessionExpired");
                    //context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //context.RedirectUri = url;
                }
            }
            context.HttpContext.Response.Redirect(context.RedirectUri);
            context.HttpContext.Response.StatusCode = 401;
            if (context.Request.Method == HttpMethods.Get)
            {
                return base.RedirectToLogin(context);
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToLogout(context);
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToReturnUrl(context);
        }
        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            return base.RedirectToAccessDenied(context);
        }
    }
}
