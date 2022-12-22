namespace BookRental_dotnet.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public bool approved { get; set; }
        public DateTime startDate { get; set; }
        public DateTime returnDate { get; set; }
        public bool returned { get; set; }
        public User user { get; set; }
        public Book book { get; set; }

    }
}
