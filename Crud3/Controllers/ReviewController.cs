using AutoMapper;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookReviewing_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id == null) 
            {
                return NotFound();
            }
            Review review = await _unitOfWork.reviewRepository.Get(filter:x=>x.Id == id,includes: new[] {"Book","Reviewer"});

            // getting all books related data and display in view
            List<Book> books = await _unitOfWork.bookRepository.GetAll();

            List<string> booksList = new List<string>();

            foreach (var book in books)
            {
                booksList.Add(book.Title);
            }
            ViewBag.bookListItem = new SelectList(booksList);

            // getting all reviewers related data and display in view
            List<Reviewer> reviewers = await _unitOfWork.reviewerRepository.GetAll();

            List<string> reviewersList = new List<string>();

            foreach (var reviewer in reviewers)
            {
                reviewersList.Add(reviewer.FirstName);
            }
            ViewBag.reviewerListItem = new SelectList(reviewersList);

            return View(review);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Review review) 
        {
            if (review == null) 
            {
                return BadRequest();
            }
            Book? book = await _unitOfWork.bookRepository.Get(filter: x => x.Title.ToLower() == review.Book.Title.ToLower());
            if (book == null)
            {
                ModelState.AddModelError("CustomError", "no book exists with this title");
            }
            Reviewer? reviewer = await _unitOfWork.reviewerRepository.Get(filter: x => x.FirstName.ToLower() == review.Reviewer.FirstName.ToLower());
            if (reviewer == null)
            {
                ModelState.AddModelError("CustomError", "no reviewer exists with this firstname");
            }
            review.Book = book;
            review.Reviewer = reviewer;

            _unitOfWork.reviewRepository.Update(review);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }
            Review review = await _unitOfWork.reviewRepository.Get(filter: x => x.Id == id);

            _unitOfWork.reviewRepository.Delete(review);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
