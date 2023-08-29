using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewing_MVC.Utilities;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.categoryRepository.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            List<CategoryDTO> categoriesDTOS = _mapper.Map<List<CategoryDTO>>(categories);
            return View(categoriesDTOS);
        }
        [Authorize(Roles =SD.Role_Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Create(CategoryCreateDTO categoryCreateDTO)
        {
            if (categoryCreateDTO == null)
            {
                return NotFound();
            }
            Category categoryInDb = await _unitOfWork.categoryRepository.Get(filter: x => x.Name.ToLower() == categoryCreateDTO.Name.ToLower());
            if (categoryInDb != null)
            {
                return BadRequest("category already exists");
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
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = await _unitOfWork.categoryRepository.Get(filter: x=>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return View(categoryDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            if (categoryUpdateDTO == null)
            {
                return BadRequest("model must't be empty");
            }
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(categoryUpdateDTO);
                _unitOfWork.categoryRepository.Update(category);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(categoryUpdateDTO);
        }
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = await _unitOfWork.categoryRepository.Get(filter: x=>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.categoryRepository.Delete(category);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
