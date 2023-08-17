using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.IRepositories;
using System.Linq.Expressions;

namespace BookReviewing_MVC.Services.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext db, ApplicationDbContext context) : base(db)
        {
            _context = context;
        }

        public Book Update(Book book)
        {
             _context.Books.Update(book);
            return book;
        }
    }
}
