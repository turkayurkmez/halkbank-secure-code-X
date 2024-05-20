using InjectionAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Encodings.Web;

namespace InjectionAttacks.Controllers
{
    public class AccountController : Controller
    {
        private readonly JavaScriptEncoder javaScriptEncoder;
        private readonly HtmlEncoder htmlEncoder;
        private readonly UrlEncoder urlEncoder;

        public AccountController(JavaScriptEncoder javaScriptEncoder)
        {
            this.javaScriptEncoder = javaScriptEncoder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginViewModel user) 
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True;");
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE UserName=@name AND Password=@pass",sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", user.UserName);
            sqlCommand.Parameters.AddWithValue("@pass", user.Password);


            sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                ViewBag.State = "Giriş Başarılı...";
                return View();
            }
            ViewBag.State = "Hatalı Giriş!";
            return View();



        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegisterViewModel userRegister)
        {
            if (ModelState.IsValid)
            {

                userRegister.UserInfo = javaScriptEncoder.Encode(userRegister.UserInfo);
                return View(userRegister);
            }

            return View();
           
        }
    }
}
