using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.Repositories;

namespace BookReviewing_MVC.Services.IRepositories
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
