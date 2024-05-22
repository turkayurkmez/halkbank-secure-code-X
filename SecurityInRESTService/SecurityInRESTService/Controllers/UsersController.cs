using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecurityInRESTService.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityInRESTService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.ValidateUser(userLoginModel.UserName, userLoginModel.Password);
                if (user != null)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-cümle-token-onayi-icin-onemli"));
                    var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var token = new JwtSecurityToken(
                        issuer: "server.halkbank",
                        audience: "client.halkbank",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credential
                        );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });






                }
                ModelState.AddModelError("loginFailed", "Hatalı giriş");
            }
            return BadRequest(ModelState);
        }
    }
}
