using AutoMapper;
using BookReviewing_MVC.DTOS;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
        {
            if (!ModelState.IsValid || bookCreateDTO == null)
            {
                ModelState.AddModelError("CustomError","book fields not valid");
            }
            Book bookfromDb = await _unitOfWork.bookRepository.Get(filter:x=>x.Title.ToLower() == bookCreateDTO.Title.ToLower());
            if(bookfromDb != null)
            {
                ModelState.AddModelError("CustomError","oops book alreay exists");
            }
            if (ModelState.IsValid)
            {
                Book book = _mapper.Map<Book>(bookCreateDTO);
                await _unitOfWork.bookRepository.Create(book);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(bookCreateDTO);
        }

        public async Task<IActionResult> Update(int bookId)
        {
            if (bookId == 0)
            {
                ModelState.AddModelError("CustomError","bookId must not be 0");
            }
            Book book = await _unitOfWork.bookRepository.Get(filter:x=>x.Id == bookId);
            if (book == null)
            {
                ModelState.AddModelError("CustomError","this book not found!");
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookUpdateDTO bookUpdateDTO)
        {
            if (!ModelState.IsValid || bookUpdateDTO == null)
            {
                ModelState.AddModelError("CustomError", "book fields not valid");
            }
            if (ModelState.IsValid)
            {
                Book book = _mapper.Map<Book>(bookUpdateDTO);
                _unitOfWork.bookRepository.Update(book);
                await _unitOfWork.save();
                return RedirectToAction("Index");
            }
            return View(bookUpdateDTO);
        }

        public async Task<IActionResult> Delete(int bookId)
        {
            if (bookId == 0)
            {
                ModelState.AddModelError("CustomError","this book not found");
            }
            Book book = await _unitOfWork.bookRepository.Get(filter:x=>x.Id == bookId);
            //if (book == null)
            //{
            //    return NotFound("book not found");
            //}
            _unitOfWork.bookRepository.Delete(book);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
