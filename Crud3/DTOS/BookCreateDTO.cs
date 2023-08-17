namespace BookReviewing_MVC.DTOS
{
    public class BookCreateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public DateTime? DatePublished { get; set; }
    }
}
