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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            if(review == null) 
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("not valid"); 
            }
            Review isReviewFound = await _unitOfWork.reviewRepository.Get(filter:x=>x.ReviewText.ToLower() ==review.ReviewText.ToLower());
            if (isReviewFound != null) 
            {
                return BadRequest("review already exists");
            }

            // related entites 
            // when create new review .. the review book we will added should be found in database.. 
            Book book = await _unitOfWork.bookRepository.Get(filter:x=>x.Title.ToLower() == review.Book.Title.ToLower());
            if (book == null)
            {
                return BadRequest("no book exists with this title");
            }
            review.Book = book;

            await _unitOfWork.reviewRepository.Create(review);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
