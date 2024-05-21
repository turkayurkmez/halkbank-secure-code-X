using System;
using System.Collections.Generic;

namespace AuthNAndAuthZ.Models;

public partial class RealUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Role { get; set; } = null!;
}
