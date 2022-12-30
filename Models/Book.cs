namespace BookRental_dotnet.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; } 
        public string Title { get; set; } 
        public string ISBN { get; set; } 
        public bool isAvailable {get; set; }
        public ICollection<Loan> Loans { get; set; } 
        public ICollection<Reservation> Reservations { get; set; } 

        public Book(Guid Id, string Author, string Title, string ISBN, bool isAvailable) {
            this.Id = Id;
            this.Author = Author;
            this.Title = Title;
            this.ISBN = ISBN;
            this.isAvailable = isAvailable;
        }
     }
}
