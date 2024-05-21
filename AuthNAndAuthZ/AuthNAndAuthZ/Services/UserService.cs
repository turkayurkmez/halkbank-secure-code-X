using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>()
        {
             new User{ Id =1, Name ="türkay", UserName="turkay", Email="a@b.com", Role="Admin", PasswordHash="123456"},
             new User{ Id =2, Name ="Sermin", UserName="sermin", Email="a@b.com", Role="Editor", PasswordHash="123456"},
             new User{ Id =3, Name ="Irem", UserName="irem", Email="a@b.com", Role="Client", PasswordHash="123456"}

        };

        public void CreateUser(RealUserViewModel userData)
        {
            throw new NotImplementedException();
        }

        public User? ValidateUser(string userName, string passWord)
        {
            return _users.SingleOrDefault(u => u.UserName == userName && u.PasswordHash == passWord);

        }

        RealUser? IUserService.ValidateUser(string userName, string passWord)
        {
            throw new NotImplementedException();
        }
    }
}
