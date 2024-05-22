using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SecurityInRESTService.Security
{
    public class BasicHandler : AuthenticationHandler<BasicOption>
    {
        public BasicHandler(IOptionsMonitor<BasicOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            /*
             *  $.ajax({
                url:'https://localhost:7041/WeatherForecast',
                headers: {
                    'Authorization':'Basic '+btoa('turkayTest:123456')
                },               
                method:'GET',
                success:function(data){
                    console.log(data);
                }
            })
             */

            /*
             * 1. Gelen istekte "Authorization" header'i var mı?
             * 2. Bu Authorization header'i standarda uygun mu?
             * 3. Authorization Basic mi?
             * 4. Şema değeri uygun mu?
             * 5. : işaretine göre ayır. İlk eleman kullanıcı ikincisi şifredir.
             * * 
             */
            //1. Gelen istekte "Authorization" header'i var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2. Bu Authorization header'i standarda uygun mu?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"],out AuthenticationHeaderValue headerValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }
            //3.Authorization Basic mi?
            if (!headerValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            //4. Şema değeri uygun mu?
            var bytes = Convert.FromBase64String(headerValue.Parameter);
            var userNameAndPass = Encoding.UTF8.GetString(bytes); //'turkayTest:123456'

            //5. : işaretine göre ayır. İlk eleman kullanıcı ikincisi şifredir.
            var userName = userNameAndPass.Split(':')[0];
            var passWord = userNameAndPass.Split(':')[1];

            if (userName == "turkayTest" && passWord == "123456")
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));

            }

            return Task.FromResult(AuthenticateResult.Fail("Hatalı giriş"));





        }
    }
}
