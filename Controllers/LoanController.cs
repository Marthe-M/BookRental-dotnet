using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        public LoanController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /*         [HttpGet]
                [Authorize(Roles="True")] 
                public async Task<IActionResult> GetAllBooksFromReservations()
                {
                   return Ok(await dbContext.Reservations
                   .Include(r => r.book)
                   .Include(r => r.user)
                   .ToListAsync());
                }
         */


        [HttpPost]
        [Authorize]
        [Route("add")]
        public async Task<IActionResult> AddLoan(AddLoan addloan)
        {
            var user = await dbContext.Users.FindAsync(addloan.UserId);
            var book = await dbContext.Books.FindAsync(addloan.BookId);
            var newLoan = new Loan()
            {
                Id = Guid.NewGuid(),
                Approved = true,
                StartDate = DateTime.Now,
                User = user,
                Book = book
            };

                await dbContext.Loans.AddAsync(newLoan);
                await dbContext.SaveChangesAsync();
         

            return Ok(newLoan);
        }


    }
}
