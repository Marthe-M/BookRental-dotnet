using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
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
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await dbContext.Users.ToListAsync()); 
           
        }
       
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {

            var userExist = await dbContext.Users.FirstOrDefaultAsync(u => u.email == addUserRequest.email);
            if(userExist == null)
            {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                firstName = addUserRequest.firstName,
                lastName = addUserRequest.lastName,
                email = addUserRequest.email,
                isAdmin = addUserRequest.isAdmin,
      
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
            }
            return BadRequest("User already exists");
        }


       
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user != null)
            {
                user.firstName = updateUserRequest.firstName;
                user.lastName = updateUserRequest.lastName;
                user.email = updateUserRequest.email;
                user.username = updateUserRequest.username;
                user.password = updateUserRequest.password;
                user.isAdmin = updateUserRequest.isAdmin;
                user.firstTimeLogin = updateUserRequest.firstTimeLogin;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }



        [HttpDelete]
        [Route("{id:guid}")]
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
