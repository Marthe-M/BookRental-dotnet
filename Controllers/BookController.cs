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
            return Ok(await dbContext.Books.ToListAsync()); 
           
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
            return Ok(book);

        }
       
        [HttpPost]
        [Authorize(Roles="True")] 
        [Route("add")]
        public async Task<IActionResult> AddBook(AddBookRequest addBookRequest) {
            var book = new Book()
            { Id = Guid.NewGuid(), Author = addBookRequest.Author, ISBN = addBookRequest.ISBN, Title = addBookRequest.Title };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return Ok(book);
        }
      
        [HttpPut]
        [Authorize(Roles="True")] 
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, UpdateBookRequest updateBookRequest)
        {
            var book = await dbContext.Books.FindAsync(id);
            
            if (book != null)
            {
                book.Author = updateBookRequest.Author;
                book.Title= updateBookRequest.Title;
                book.ISBN = updateBookRequest.ISBN;

                await dbContext.SaveChangesAsync();

                return Ok(book);
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
