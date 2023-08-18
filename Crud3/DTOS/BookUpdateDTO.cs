using System.ComponentModel.DataAnnotations;

namespace BookReviewing_MVC.DTOS
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
