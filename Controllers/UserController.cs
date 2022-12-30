using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        public UserController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      
        [HttpGet]
         [Authorize(Roles="True")] 
        public async Task<IActionResult> GetAllBooks()
        {
            var users = await dbContext.Users.ToListAsync();
            var dtos = from u in users select new UserResponseDTO(u.Id, u.firstName, u.lastName, u.email, u.username,
                                                              u.password, u.isAdmin, u.firstTimeLogin);
            return Ok(dtos);
        }
       
        [HttpGet]
         [Authorize(Roles="True")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(new UserResponseDTO(user.Id, user.firstName, user.lastName, user.email, user.username,
                                          user.password, user.isAdmin, user.firstTimeLogin));
        }

        [HttpPost]
         [Authorize(Roles="True")]
        [Route("add")]
        public async Task<IActionResult> AddUser(UserAddDTO dto)
        {
            var userExist = await dbContext.Users.FirstOrDefaultAsync(u => u.email == dto.email);
            if(userExist == null)
            {
            var user = new User(Guid.NewGuid(), dto.firstName, dto.lastName, dto.email, String.Empty, String.Empty, dto.isAdmin, true);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(new UserResponseDTO(user.Id, user.firstName, user.lastName, user.email, user.username,
                                          user.password, user.isAdmin, user.firstTimeLogin));
            }
            return BadRequest("User already exists");
        }


       
        [HttpPut]
        [Route("{id:guid}")]
         [Authorize(Roles="True")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UserUpdateDTO dto)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user != null)
            {
                user.firstName = dto.firstName;
                user.lastName = dto.lastName;
                user.email = dto.email;
                user.username = dto.username;
                user.password = dto.password;
                user.isAdmin = dto.isAdmin;
                user.firstTimeLogin = dto.firstTimeLogin;

                await dbContext.SaveChangesAsync();

                return Ok(new UserResponseDTO(user.Id, user.firstName, user.lastName, user.email, user.username,
                                              user.password, user.isAdmin, user.firstTimeLogin));
            }
            return NotFound();
        }



        [HttpDelete]
        [Route("{id:guid}")]
         [Authorize(Roles="True")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if(user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

      
    }
}
