namespace BookRental_dotnet.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Author { get; set; } 
        public string Title { get; set; } 
        public string ISBN { get; set; } 
        public bool isAvailable {get; set; }
        public Loan Loan {get; set; }
        public ICollection<Reservation> Reservations { get; set; } 

     }
}
