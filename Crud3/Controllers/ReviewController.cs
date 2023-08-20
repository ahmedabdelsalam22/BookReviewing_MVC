using AutoMapper;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Review> reviews = await _unitOfWork.reviewRepository.GetAll(includeProperties: "Book");
            if (reviews == null)
            {
                return BadRequest();
            }
            return View(reviews);
        }
    }
}
