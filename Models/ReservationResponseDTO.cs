namespace BookRental_dotnet.Models
{
    public class ReservationResponseDTO
    {
        public Guid id { get; }
        public bool approved { get; }
        public User user { get; }
        public Book book { get; }

        public ReservationResponseDTO(Guid id, bool approved, User user, Book book) {
            this.id = id;
            this.approved = approved;
            this.user = user;
            this.book = book;
        }
     }
}
