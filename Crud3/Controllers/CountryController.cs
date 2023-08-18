using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Country> countries = await _unitOfWork.countryRepository.GetAll();
            if (countries == null)
            {
                return NotFound();
            }
            List<CountryDTO> countryDTOs = _mapper.Map<List<CountryDTO>>(countries);
            return View(countryDTOs);
        }
    }
}
