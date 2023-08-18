namespace BookReviewing_MVC.DTOS
{
    public class BookUpdateDTO
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
