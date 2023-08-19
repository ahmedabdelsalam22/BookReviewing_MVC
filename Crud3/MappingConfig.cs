using AutoMapper;
using BookReviewing_MVC.DTOS;
using BookReviewingMVC.Models;

namespace BookReviewing_MVC
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<BookCreateDTO, Book>();
            CreateMap<BookUpdateDTO, Book>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<CountryCreateDTO, Country>();
            CreateMap<CountryUpdateDTO, Country>();
            CreateMap<Reviewer, ReviewerDTO>();
            CreateMap<ReviewerCreateDTO, Reviewer>();
            CreateMap<ReviewerUpdateDTO, Reviewer>();
        }
    }
}
