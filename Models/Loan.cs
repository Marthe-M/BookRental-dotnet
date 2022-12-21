namespace BookRental_dotnet.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public bool Approved { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }

    }
}
