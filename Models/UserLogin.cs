namespace BookRental_dotnet.Models
{
    public class UserLogin
    {
        public string username { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
        public bool isAdmin { get; set; } = false; 
  
     }
}
