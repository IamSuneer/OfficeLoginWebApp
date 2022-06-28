using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using OfficeLoginWebApp.Services;

namespace OfficeLoginWebApp.Controllers
{
    public class SignoutController : Controller
    {
        IDataService _ids;

        public SignoutController(IDataService ids)
        {
            _ids = ids;
        }

        public IActionResult SignOut([FromRoute] string scheme)
        {
            _ids.logoutEntry(User.Identity.Name);

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            scheme ??= OpenIdConnectDefaults.AuthenticationScheme;
            //var callbackUrl = Url.Page("", pageHandler: null, values: null, protocol: Request.Scheme);
            return SignOut(
                 new AuthenticationProperties
                 {
                     RedirectUri = "",
                 },
                 CookieAuthenticationDefaults.AuthenticationScheme, scheme);
        }
    }
}
