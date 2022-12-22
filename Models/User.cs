namespace BookRental_dotnet.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public bool firstTimeLogin { get; set; }

        public User(Guid Id, string firstName, string lastName, string email, string username, string password, bool isAdmin, bool firstTimeLogin) {
            this.Id = Id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
            this.firstTimeLogin = firstTimeLogin;
        }
    }
}
