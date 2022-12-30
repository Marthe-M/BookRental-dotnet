namespace BookRental_dotnet.Models
{
    public class ReservationRequestDTO
    {
        public bool approved { get; }
        public Guid userId { get; }
        public Guid bookId { get; }

        public ReservationRequestDTO(bool approved, Guid userId, Guid bookId) {
            this.approved = approved;
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
