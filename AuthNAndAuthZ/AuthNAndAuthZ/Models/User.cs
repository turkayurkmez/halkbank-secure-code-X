﻿namespace AuthNAndAuthZ.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        //RBAC: Role based account control
        public string Role { get; set; }

    }
}
