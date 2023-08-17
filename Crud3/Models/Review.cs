using System;
using System.Collections.Generic;

namespace BookReviewing_MVC.Models;

public partial class Review
{
    public int Id { get; set; }

    public string HeadLine { get; set; } = null!;

    public string ReviewText { get; set; } = null!;

    public int Rating { get; set; }

    public int BookId { get; set; }

    public int ReviewerId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reviewer Reviewer { get; set; } = null!;
}
