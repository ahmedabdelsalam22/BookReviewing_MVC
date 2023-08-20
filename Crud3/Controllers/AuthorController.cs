﻿using AutoMapper;
using BookReviewing_MVC.Services.IRepositories;
using BookReviewingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Author> authors = await _unitOfWork.authorRepository.GetAll(includeProperties: "Country");
            if (authors == null)
            {
                return NotFound();
            }
            return View(authors);
        }

        public async Task<IActionResult> Create()
        {
            List<Country> countries = await _unitOfWork.countryRepository.GetAll();

            ViewBag.countriesListItem = new SelectList(countries, "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("please fill all data");
            }
            Author authorIsFound = await _unitOfWork.authorRepository.Get(filter: x=>x.FirstName.ToLower() == author.FirstName.ToLower());
            if(authorIsFound != null)
            {
                return BadRequest("this author arleady exists");
            }

            // option 1 - we added country manualy ..
            // when create new author .. the author country we will added should be found in database.. 
            Country country = await _unitOfWork.countryRepository.Get(filter: x=>x.Name.ToLower() == author.Country.Name.ToLower());
            if (country == null)
            {
                return NotFound("country does't exists");
            }
            author.Country = country;
            
            await _unitOfWork.authorRepository.Create(author);
            await _unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
