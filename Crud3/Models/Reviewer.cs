using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class Reviewer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
