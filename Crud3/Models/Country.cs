using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
