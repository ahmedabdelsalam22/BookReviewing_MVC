using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;

namespace BookReviewing_MVC.Services.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
