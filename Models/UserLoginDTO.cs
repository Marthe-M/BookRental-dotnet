namespace BookRental_dotnet.Models
{
    public class UserLoginDTO
    {
        public string username { get; }
        public string password { get; }

        public UserLoginDTO(string username, string password) {
            this.username = username;
            this.password = password;
        }
    }
}
