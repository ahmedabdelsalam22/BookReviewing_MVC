namespace BookReviewing_MVC.Services.IRepositories
{
    public interface IUnitOfWork
    {
        public IBookRepository bookRepository { get; }

        Task save();
    }
}
