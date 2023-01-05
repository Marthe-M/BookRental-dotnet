namespace BookRental_dotnet.Models
{
    public class AddReservationRequest
    {
        public bool approved { get; set; }
        public Guid userId { get; set; }
        public Guid bookId { get; set; }
    }
}
