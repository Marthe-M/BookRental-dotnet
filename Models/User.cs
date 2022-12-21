namespace BookRental_dotnet.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; } = String.Empty;
        public string lastName { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public string username { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
        public bool isAdmin { get; set; } = false;
        public bool firstTimeLogin { get; set; } = true;
        public Loan Loan { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
