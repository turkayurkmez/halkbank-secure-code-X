using DataProtectionOnServer.Models;
using DataProtectionOnServer.Security;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataProtectionOnServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*Sunucuya güvenme! Kritik verileri şifrele.... */
            string value = "Bu cümle bizim için çok önemli";
            DataProtector protector = new DataProtector("crypt.txt");
            var encryptedLength = protector.EncryptData(value);
            var decryptedValue = protector.DecryptData(encryptedLength);

            ViewBag.Value = decryptedValue;


            return View();
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
}
