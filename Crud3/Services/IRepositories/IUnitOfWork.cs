namespace BookReviewing_MVC.Services.IRepositories
{
    public interface IUnitOfWork
    {
        public IBookRepository bookRepository { get; }
        public ICategoryRepository categoryRepository { get; }

        Task save();
    }
}
