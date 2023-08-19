using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class ReviewerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ReviewerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Reviewer> reviewers = await _unitOfWork.reviewerRepository.GetAll();
            if (reviewers == null)
            {
                return BadRequest();
            }
            List<ReviewerDTO> reviewerDTOs = _mapper.Map<List<ReviewerDTO>>(reviewers);
            return View(reviewerDTOs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewerCreateDTO reviewerCreateDTO)
        {
            if (reviewerCreateDTO == null)
            {
                return NotFound();
            }
            Reviewer reviewerIsFound = await _unitOfWork.reviewerRepository.Get(filter: x => x.FirstName.ToLower() == reviewerCreateDTO.FirstName.ToLower());
            if (reviewerIsFound != null)
            {
                return BadRequest("reviewer already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Reviewer reviewerToDB = _mapper.Map<Reviewer>(reviewerCreateDTO);
                await _unitOfWork.reviewerRepository.Create(reviewerToDB);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(reviewerCreateDTO);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Reviewer reviewer = await _unitOfWork.reviewerRepository.Get(filter: x => x.Id == id);
            if (reviewer == null)
            {
                ModelState.AddModelError("CustomError", "this reviewer not found!");
            }
            return View(reviewer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ReviewerUpdateDTO reviewerUpdateDTO)
        {
            if (!ModelState.IsValid || reviewerUpdateDTO == null)
            {
                ModelState.AddModelError("CustomError", "reviewer fields not valid");
            }
            if (ModelState.IsValid)
            {
                Reviewer reviewer = _mapper.Map<Reviewer>(reviewerUpdateDTO);
                _unitOfWork.reviewerRepository.Update(reviewer);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(reviewerUpdateDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                ModelState.AddModelError("CustomError", "this reviewer not found");
            }
            Reviewer reviewer = await _unitOfWork.reviewerRepository.Get(filter: x => x.Id == id);
         
            _unitOfWork.reviewerRepository.Delete(reviewer);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
