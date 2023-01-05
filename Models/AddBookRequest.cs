namespace BookRental_dotnet.Models
{
    public class AddBookRequest
    {
        public string Author { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string ISBN { get; set; } = String.Empty;
        public bool isAvailable {get; set; }

    }
}
