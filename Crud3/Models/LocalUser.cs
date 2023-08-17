using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class LocalUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
