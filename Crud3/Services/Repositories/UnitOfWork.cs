using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;

namespace BookReviewing_MVC.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            bookRepository = new BookRepository(_context);
            categoryRepository = new CategoryRepository(_context);
            countryRepository = new CountryRepository(_context);
        }
        public IBookRepository bookRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }

        public ICountryRepository countryRepository { get; private set; }

        public async Task save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
