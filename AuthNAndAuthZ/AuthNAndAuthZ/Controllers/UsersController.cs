using AuthNAndAuthZ.Models;
using AuthNAndAuthZ.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthNAndAuthZ.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login(string? gidilecekUrl)
        {

            UserLoginViewModel userLogin = new UserLoginViewModel();
            userLogin.ReturnUrl = gidilecekUrl;
            return View(userLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = userService.ValidateUser(userLogin.UserName, userLogin.Password);
                if (user != null)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role,user.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    if (!string.IsNullOrEmpty(userLogin.ReturnUrl) && Url.IsLocalUrl(userLogin.ReturnUrl))
                    {
                        return Redirect(userLogin.ReturnUrl);
                    }
                    return Redirect("/");
                }

                ModelState.AddModelError("failed", "Hatalı giriş denemesi...");
                
            }

            return View(userLogin);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }

        public IActionResult AccessDenied() => View();
        public IActionResult RegisterUser() => View();
        [HttpPost]
        public IActionResult RegisterUser(RealUserViewModel realUser) 
        {
            if (ModelState.IsValid)
            {
                userService.CreateUser(realUser);
                return Redirect("/");
            }
            return View();

        }
    }
}
