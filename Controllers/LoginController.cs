using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        private readonly IConfiguration configuration;
        public LoginController(BookAPIDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLoginDTO dto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.username == dto.username);

            if (user != null)
            {
                bool verified = BCrypt.Net.BCrypt.Verify(dto.password, user.password);
                if (verified)
                {
                    string token = CreateToken(user);
                    return Ok(token);
                }
            }

            return NotFound();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.isAdmin.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
