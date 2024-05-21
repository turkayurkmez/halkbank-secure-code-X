using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNAndAuthZ.Controllers
{
    public class ForbiddenController : Controller
    {
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
