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
    
    public class BookController : Controller
    {
        private readonly BookAPIDbContext dbContext;

        public BookController(BookAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      
        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await dbContext.Books.ToListAsync();
            var dtos = from b in dbContext.Books
                       select new BookResponseDTO(b.Id, b.Author, b.Title, b.ISBN, b.isAvailable);
            return Ok(dtos);
        }
       
        [HttpGet]
        [Authorize] 
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(new BookResponseDTO(book.Id, book.Author, book.Title, book.ISBN, book.isAvailable));
        }
       
        [HttpPost]
        [Authorize(Roles="True")] 
        [Route("add")]
        public async Task<IActionResult> AddBook(BookRequestDTO dto) {
            var book = new Book(Guid.NewGuid(), dto.Author, dto.Title, dto.ISBN, true);
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return Ok(new BookResponseDTO(book.Id, book.Author, book.Title, book.ISBN, book.isAvailable));
        }
      
        [HttpPut]
        [Authorize(Roles="True")] 
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, BookRequestDTO dto)
        {
            var book = await dbContext.Books.FindAsync(id);
            
            if (book != null)
            {
                book.Author = dto.Author;
                book.Title= dto.Title;
                book.ISBN = dto.ISBN;
                book.isAvailable = dto.isAvailable;

                await dbContext.SaveChangesAsync();

                return Ok(new BookResponseDTO(book.Id, book.Author, book.Title, book.ISBN, book.isAvailable));
            }
            return NotFound();
        }
      
        [HttpDelete]
        [Authorize(Roles="True")] 
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if(book != null)
            {
                dbContext.Remove(book);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

      
    }
}
