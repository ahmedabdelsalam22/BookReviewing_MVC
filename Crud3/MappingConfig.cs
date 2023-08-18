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
        }
    }
}
