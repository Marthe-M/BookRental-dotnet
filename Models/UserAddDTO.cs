namespace BookRental_dotnet.Models
{
    public class UserAddDTO
    {
        public string firstName { get; }
        public string lastName { get; }
        public string email { get; }
        public bool isAdmin { get; }

        public UserAddDTO(string firstName, string lastName, string email, bool isAdmin) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.isAdmin = isAdmin;
        }
    }
}
