using BookRental_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRental_dotnet.Data
{
    public class BookAPIDbContext : DbContext
    {
        public BookAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
