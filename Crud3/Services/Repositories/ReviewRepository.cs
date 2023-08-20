using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;

namespace BookReviewing_MVC.Services.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
