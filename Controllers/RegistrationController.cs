using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using Microsoft.AspNetCore.Cors;
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

            if (user != null)
            {
                user.username = userRegistration.username;
                user.password = userRegistration.password;
                user.isAdmin = userRegistration.isAdmin;
                user.firstTimeLogin = false;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }

    }
}