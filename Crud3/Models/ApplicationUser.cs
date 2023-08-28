
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace BookReviewing_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
