namespace SecurityInRESTService.Services
{
    public class User
    {
        public int Id{ get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class UserService
    {
        private List<User> _users = new List<User>()
        {
            new User{ Id=1, UserName="turkay", Password="1234", Role="Admin"},
            new User{ Id=2, UserName="ozkan", Password="1234", Role="Editor"},
            new User{ Id=3, UserName="gokce", Password="1234", Role="Client"},

        };

        public User? ValidateUser(string userName,  string password)
        {
            return _users.SingleOrDefault(u=>u.UserName == userName && u.Password == password);
        }
    }
}
