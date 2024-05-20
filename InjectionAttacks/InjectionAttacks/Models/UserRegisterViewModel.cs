using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace InjectionAttacks.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="E-posta adresi boş olamaz")]
        [EmailAddress(ErrorMessage = "Eposta formatı doğru değil!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre boş olamaz")]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        [Required]
        [MaxLength(70)]
        public string UserName { get; set; }

        [DataType(DataType.MultilineText)]
        public string UserInfo { get; set; }
    }
}
