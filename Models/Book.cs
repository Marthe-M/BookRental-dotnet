namespace BookRental_dotnet.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }

        public Book(Guid Id, string Author, string Title, string ISBN) {
            this.Id = Id;
            this.Author = Author;
            this.Title = Title;
            this.ISBN = ISBN;
        }
     }
}
