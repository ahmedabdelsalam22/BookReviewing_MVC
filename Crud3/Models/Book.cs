using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public DateTime? DatePublished { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Author>? Authors { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
}
