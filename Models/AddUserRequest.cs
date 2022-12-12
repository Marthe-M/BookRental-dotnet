namespace BookRental_dotnet.Models
{
    public class AddUserRequest
    {
        public string firstName { get; set; } = String.Empty;
        public string lastName { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;

        public bool isAdmin { get; set; } = false;
    }
}
