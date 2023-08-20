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
            IEnumerable<Review> reviews = await _unitOfWork.reviewRepository.GetAll(includes: new[] {"Book","Reviewer"});
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
                ModelState.AddModelError("CustomError","not valid");
            }
            Review isReviewFound = await _unitOfWork.reviewRepository.Get(filter:x=>x.ReviewText.ToLower() ==review.ReviewText.ToLower());
            if (isReviewFound != null) 
            {
                ModelState.AddModelError("CustomError","review already exists");
            }
            // related entites 
            // when create new review .. the review book we will added should be found in database.. 
            Book? book = await _unitOfWork.bookRepository.Get(filter:x=>x.Title.ToLower() == review.Book.Title.ToLower());
            if (book == null)
            {
                ModelState.AddModelError("CustomError", "no book exists with this title");
            }
            Reviewer? reviewer = await _unitOfWork.reviewerRepository.Get(filter:x=>x.FirstName.ToLower() == review.Reviewer.FirstName.ToLower());
            if (reviewer == null)
            {
                ModelState.AddModelError("CustomError", "no reviewer exists with this firstname");
            }
            review.Book = book;
            review.Reviewer = reviewer;

            await _unitOfWork.reviewRepository.Create(review);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }

         
    }
}
