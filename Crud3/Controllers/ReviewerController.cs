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
    }
}
