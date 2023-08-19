using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;

namespace BookReviewing_MVC.Services.Repositories
{
    public class ReviewerRepository : Repository<Reviewer>, IReviewerRepository
    {
        public ReviewerRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
