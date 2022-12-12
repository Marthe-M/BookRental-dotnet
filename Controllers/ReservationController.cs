using BookRental_dotnet.Data;
using BookRental_dotnet.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookRental_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        public ReservationController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddReservation(AddReservationRequest addReservationRequest) {
            var user = await dbContext.Users.FindAsync(addReservationRequest.userId);
            var book = await dbContext.Books.FindAsync(addReservationRequest.bookId);
            var reservation = new Reservation()
            { id = Guid.NewGuid(), approved = addReservationRequest.approved, user = user, book = book };
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();
            return Ok(reservation);
        }
    }
}
