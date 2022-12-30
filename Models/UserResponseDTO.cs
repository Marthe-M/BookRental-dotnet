﻿namespace BookRental_dotnet.Models
{
    public class UserResponseDTO
    {
        public Guid Id { get; }
        public string firstName { get; }
        public string lastName { get; }
        public string email { get; }
        public string username { get; }
        public string password { get; }
        public bool isAdmin { get; }
        public bool firstTimeLogin { get; }

        public UserResponseDTO(Guid Id, string firstName, string lastName, string email, string username, string password, bool isAdmin, bool firstTimeLogin) {
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
