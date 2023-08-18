using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.IRepositories;
using System.Linq.Expressions;

namespace BookReviewing_MVC.Services.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
    }
}
