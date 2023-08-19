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
    }
}
