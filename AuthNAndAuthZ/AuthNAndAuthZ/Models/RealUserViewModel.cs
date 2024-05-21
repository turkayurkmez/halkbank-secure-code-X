using System.ComponentModel.DataAnnotations;

namespace AuthNAndAuthZ.Models
{
    public class RealUserViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string ClearPassword { get; set; }
        
        //RBAC: Role based account control

    }
}
