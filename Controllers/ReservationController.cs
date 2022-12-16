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
            { id = Guid.NewGuid(), approved = addReservationRequest.approved, user = user, book = book};

            if (!dbContext.Reservations.Any(r => r.user == user && r.book == book)) {
                await dbContext.Reservations.AddAsync(reservation);
                await dbContext.SaveChangesAsync();
            }

            return Ok(reservation);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if(reservation != null)
            {
                dbContext.Remove(reservation);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetReservations([FromRoute] Guid id)
        {
            var reservations = await dbContext.Reservations.Where(r => r.user.Id == id).Include(r => r.book).ToListAsync();
            return Ok(reservations);
        }
    }
}
