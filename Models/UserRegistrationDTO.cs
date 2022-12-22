namespace BookRental_dotnet.Models
{
    public class UserRegistrationDTO
    {
        public string email { get; }
        public string username { get; }
        public string password { get; }

        public UserRegistrationDTO(string email, string username, string password) {
            this.email = email;
            this.username = username;
            this.password = password;
        }
    }
}
