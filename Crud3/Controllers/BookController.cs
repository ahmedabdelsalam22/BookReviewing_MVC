using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _unitOfWork._bookRepository.GetAll();
            return View(books);
        }
    }
}
