namespace BookRental_dotnet.Models
{
    public class BookRequestDTO
    {
        public string Author { get; }
        public string Title { get; }
        public string ISBN { get; }
        public bool isAvailable { get; }

        public BookRequestDTO(string Author, string Title, string ISBN, bool isAvailable) {
            this.Author = Author;
            this.Title = Title;
            this.ISBN = ISBN;
            this.isAvailable = isAvailable;
        }
    }
}
