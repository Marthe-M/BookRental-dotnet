namespace BookRental_dotnet.Models
{
    public class BookResponseDTO
    {
        public Guid Id { get; }
        public string Author { get; }
        public string Title { get; }
        public string ISBN { get; }
        public bool isAvailable { get; }

        public BookResponseDTO(Guid Id, string Author, string Title, string ISBN, bool isAvailable) {
            this.Id = Id;
            this.Author = Author;
            this.Title = Title;
            this.ISBN = ISBN;
            this.isAvailable = isAvailable;
        }
     }
}
