namespace BookReviewing_MVC.Services.IRepositories
{
    public interface IUnitOfWork
    {
        public IBookRepository _bookRepository { get; }

        Task save();
    }
}
