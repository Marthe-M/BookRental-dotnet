using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly BookAPIDbContext dbContext;
        public RegistrationController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }




        [HttpPut]
        public async Task<IActionResult> RegisterUser(UserRegistration userRegistration)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.email == userRegistration.email);

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegistration.password);

            if (user != null)
            {
                user.username = userRegistration.username;
                user.password = passwordHash;
                user.firstTimeLogin = false;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }

    }
}