using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewing_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _unitOfWork.bookRepository.GetAll();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError","please fill all fields");
            }
            Book bookfromDb = await _unitOfWork.bookRepository.Get(filter:x=>x.Title.ToLower() == bookCreateDTO.Title.ToLower());
            if(bookfromDb != null)
            {
                ModelState.AddModelError("CustomError","oops book alreay exists");
            }
            Book book = _mapper.Map<Book>(bookCreateDTO);
            await _unitOfWork.bookRepository.Create(book);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int bookId)
        {
            Book book = await _unitOfWork.bookRepository.Get(filter:x=>x.Id == bookId);
            if (book == null)
            {
                ModelState.AddModelError("CustomError","this book not found!");
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Update()
        {

        }
    }
}
