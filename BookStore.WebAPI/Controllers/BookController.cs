using BookStore.Common;
using BookStore.Model;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? genre)
    {
        BookService service = new BookService();
        BookFilter filter = new BookFilter(genre);
        
        List<Book>? books = await service.GetAllAsync(filter);
        if (books == null)
        {
            return NotFound();
        }
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        BookService service = new BookService();
        Book? book = await service.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Book book)
    {
        BookService service = new BookService();
        if (!await service.AddAsync(book))
        {
            return BadRequest();
        }
        return Ok("Book added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Book book)
    {
        BookService service = new BookService();
        if (!await service.Update(id, book))
        {
            return BadRequest();
        }
        return Ok("Book updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        BookService service = new BookService();
        if (!await service.Delete(id))
        {
            return BadRequest();
        }
        return Ok("Book deleted");
    }
}