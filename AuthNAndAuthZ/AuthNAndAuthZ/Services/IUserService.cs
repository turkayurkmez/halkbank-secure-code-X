using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public interface IUserService
    {
        RealUser? ValidateUser(string userName, string passWord);
        void CreateUser(RealUserViewModel userData);

      
    }
}