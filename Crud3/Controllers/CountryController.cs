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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryCreateDTO countryCreateDTO)
        {
            if (countryCreateDTO == null)
            {
                return NotFound();
            }
            Country countryIsFound = await _unitOfWork.countryRepository.Get(filter:x=>x.Name.ToLower() == countryCreateDTO.Name.ToLower());
            if (countryIsFound != null)
            {
                return BadRequest("country already exists");
            }
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Country countryToDB = _mapper.Map<Country>(countryCreateDTO);
                await _unitOfWork.countryRepository.Create(countryToDB);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(countryCreateDTO);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id == 0 || id == null)
            {
                return BadRequest();
            }
            Country country = await _unitOfWork.countryRepository.Get(filter: x=>x.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            CountryDTO countryDTO = _mapper.Map<CountryDTO>(country);
            return View(countryDTO);
        }
    }
}
