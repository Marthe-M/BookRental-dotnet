using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly BookAPIDbContext dbContext;
        public LoginController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLogin userLogin)
        {
           var user = await dbContext.Users.FirstOrDefaultAsync(u => u.username == userLogin.username);
            
            if (user != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(userLogin.password, user.password);
                if (verified) {
                    return Ok(user);
                }
            }

          return NotFound();
        }

    }
}