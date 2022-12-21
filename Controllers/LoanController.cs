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
            .Include(l => l.Book)
            .Include(l => l.User)
            .ToListAsync());
        }

        [HttpGet]
        [Authorize] 
        [Route("{id:guid}")]
        public async Task<IActionResult> GetLoanByUserId([FromRoute] Guid id)
        {
            var loans = await dbContext.Loans.Where(l => l.User.Id == id).Include(l => l.Book).ToListAsync();
            return Ok(loans);
        }


    }
}
