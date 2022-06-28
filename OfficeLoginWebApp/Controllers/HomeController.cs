using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OfficeLoginWebApp.Models;
using OfficeLoginWebApp.Services;
using System.Diagnostics;

namespace OfficeLoginWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public const string SessionKeySaved = "CheckData";

        private readonly ILogger<HomeController> _logger;
        IDataService _ids;
        public HomeController(ILogger<HomeController> logger, IDataService ids)
        {
            _logger = logger;
            _ids = ids;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeySaved)))
            {
                _ids.LoginEntry(User.Identity.Name , Request.HttpContext.Connection.RemoteIpAddress.ToString());
                HttpContext.Session.SetString(SessionKeySaved, "Saved");
            }

            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}