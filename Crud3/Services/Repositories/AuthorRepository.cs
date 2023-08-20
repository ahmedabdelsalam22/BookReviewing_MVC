using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;

namespace BookReviewing_MVC.Services.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
