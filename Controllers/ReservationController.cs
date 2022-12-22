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
        public async Task<IActionResult> AddReservation(ReservationRequestDTO dto) {
            var user = await dbContext.Users.FindAsync(dto.userId);
            var book = await dbContext.Books.FindAsync(dto.bookId);
            var reservation = new Reservation(Guid.NewGuid(), dto.approved);
            reservation.user = user;
            reservation.book = book;

            if (!dbContext.Reservations.Any(r => r.user == user && r.book == book)) {
                await dbContext.Reservations.AddAsync(reservation);
                await dbContext.SaveChangesAsync();
            }

            return Ok(new ReservationResponseDTO(reservation.id, reservation.approved, reservation.user, reservation.book));
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
            var dtos = from r in reservations select new ReservationResponseDTO(r.id, r.approved, r.user, r.book);
            return Ok(dtos);
        }
    }
}
