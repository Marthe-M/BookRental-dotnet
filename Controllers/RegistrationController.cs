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
        public async Task<IActionResult> RegisterUser(UserRegistrationDTO dto)
        {
            var userExist = await dbContext.Users.FirstOrDefaultAsync(u => u.username == dto.username);
            if (userExist == null)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.email == dto.email);

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password);

                if (user != null)
                {
                    user.username = dto.username;
                    user.password = passwordHash;
                    user.firstTimeLogin = false;

                    await dbContext.SaveChangesAsync();

                    return Ok(new UserResponseDTO(user.Id, user.firstName, user.lastName, user.email, user.username,
                                                  user.password, user.isAdmin, user.firstTimeLogin));
                }
                return NotFound();
            }
            return BadRequest("User already exists");
        }
      
    }
}
