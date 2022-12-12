namespace BookRental_dotnet.Models
{
    public class Reservation
    {
        public Guid id { get; set; }
        public bool approved { get; set; }
        public User user { get; set; }
        public Book book { get; set; }
     }
}
