using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.categoryRepository.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return NotFound();
            }
            Category categoryInDb = await _unitOfWork.categoryRepository.Get(filter: x => x.Name.ToLower() == categoryCreateDTO.Name.ToLower());
            if (categoryInDb != null)
            {
                ModelState.AddModelError("CustomError", "categoty already exists");
            }
            Category category = _mapper.Map<Category>(categoryCreateDTO);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError","model is't valid");
            }
            await _unitOfWork.categoryRepository.Create(category);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? categoryId)
        {
            if (categoryId == 0 || categoryId == null)
            {
                return NotFound();
            }
            Category category = await _unitOfWork.categoryRepository.Get(filter: x=>x.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return View(categoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            if (categoryUpdateDTO == null)
            {
                return BadRequest("model must't be empty");
            }
            Category category = _mapper.Map<Category>(categoryUpdateDTO);
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Update(category);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(categoryUpdateDTO);
        }

        public async Task<IActionResult> Delete(int? categoryId)
        {
            if (categoryId == 0 || categoryId == null)
            {
                return NotFound();
            }
            Category category = await _unitOfWork.categoryRepository.Get(filter: x=>x.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.categoryRepository.Delete(category);
            await _unitOfWork.save();
            return NoContent();
        }
    }
}
