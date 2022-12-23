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

        [HttpGet]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> GetAllLoans()
        {
            return Ok(await dbContext.Loans
            .Include(l => l.book)
            .Include(l => l.user)
            .ToListAsync());
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetLoanByUserId([FromRoute] Guid id)
        {
            var loans = await dbContext.Loans.Where(l => l.user.Id == id)
            .Include(l => l.book)
            .Include(l => l.user)
            .ToListAsync();
            return Ok(loans);
        }

        [HttpPost]
        [Authorize(Roles = "True")]
        [Route("add")]
        public async Task<IActionResult> AddLoan(AddLoan addLoan)
        {
            var user = await dbContext.Users.FindAsync(addLoan.UserId);
            var book = await dbContext.Books.FindAsync(addLoan.BookId);
            var loan = new Loan()
            {
                Id = Guid.NewGuid(),
                startDate = DateTime.Now,
                returnDate = DateTime.MinValue,
                user = user,
                book = book
            };
            book.isAvailable = false;

            if (!dbContext.Loans.Any(l => l.book.Id == addLoan.BookId && l.startDate > l.returnDate))
            {
                await dbContext.Loans.AddAsync(loan);
                await dbContext.SaveChangesAsync();
                return Ok(loan);
            }
            return BadRequest("Book is not available at the moment");

        }

        [HttpPut]
        [Authorize(Roles = "True")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateLoan([FromRoute] Guid id, AddLoan addLoan)
        {
            var loan = await dbContext.Loans.FindAsync(id);
            var book = await dbContext.Books.FindAsync(addLoan.BookId);

            if (loan != null && book != null)
            {
                loan.returnDate = DateTime.Now;
                book.isAvailable = addLoan.isAvailable;
                await dbContext.SaveChangesAsync();
                return Ok(loan);

            }
            return BadRequest("Book not found");
        }


    }
}
