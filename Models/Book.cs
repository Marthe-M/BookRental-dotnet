namespace BookRental_dotnet.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string ISBN { get; set; } = String.Empty;

     }
}
