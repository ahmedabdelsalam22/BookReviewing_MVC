using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
