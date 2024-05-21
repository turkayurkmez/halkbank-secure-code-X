using AuthNAndAuthZ.Data;
using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public class RealUserService : IUserService
    {
        private readonly SecureDbContext dbContext;

        public RealUserService(SecureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public RealUser? ValidateUser(string userName, string passWord)
        {
            var user = dbContext.RealUsers.SingleOrDefault(u => u.UserName == userName);
            if (user != null) 
            {
                BCrypt.Net.BCrypt.Verify(passWord, user.PasswordHash);
                return user;
            }
            return null;
        }

        public void CreateUser(RealUserViewModel userData)
        {
            var user = new RealUser
            {
                Email = userData.Email,
                UserName = userData.UserName,
                Name = userData.UserName,
                Role = "Client"
            };

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userData.ClearPassword);

            user.PasswordHash = passwordHash;
            dbContext.RealUsers.Add(user);
            dbContext.SaveChanges();
            
        }
    }
}
