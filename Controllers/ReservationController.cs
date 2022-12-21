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
    public class ReservationController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        public ReservationController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles="True")] 
        public async Task<IActionResult> GetAllReservations()
        {
           return Ok(await dbContext.Reservations
           .Include(r => r.book)
           .Include(r => r.user)
           .ToListAsync());
        }



        [HttpPost]
        [Authorize] 
        [Route("add")]
        public async Task<IActionResult> AddReservation(AddReservationRequest addReservationRequest)
        {
            var user = await dbContext.Users.FindAsync(addReservationRequest.userId);
            var book = await dbContext.Books.FindAsync(addReservationRequest.bookId);
            var reservation = new Reservation()
            { Id = Guid.NewGuid(), approved = addReservationRequest.approved, user = user, book = book };

            if (!dbContext.Reservations.Any(r => r.user == user && r.book == book))
            {
                await dbContext.Reservations.AddAsync(reservation);
                await dbContext.SaveChangesAsync();
            }

            return Ok(reservation);
        }

        [HttpDelete]
        [Authorize] 
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation != null)
            {
                dbContext.Remove(reservation);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize] 
        [Route("{id:guid}")]
        public async Task<IActionResult> GetReservations([FromRoute] Guid id)
        {
            var reservations = await dbContext.Reservations.Where(r => r.user.Id == id).Include(r => r.book).ToListAsync();
            return Ok(reservations);
        }
    }
}
