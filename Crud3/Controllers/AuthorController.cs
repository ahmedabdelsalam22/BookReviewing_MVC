using AutoMapper;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
