using BookStore.Common;
using BookStore.Model;
using BookStore.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? genre)
    {
        BookFilter filter = new BookFilter(genre);
        
        List<Book>? books = await _service.GetAllAsync(filter);
        if (books == null)
        {
            return NotFound();
        }
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Book? book = await _service.GetAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Book book)
    {
        if (!await _service.AddAsync(book))
        {
            return BadRequest();
        }
        return Ok("Book added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Book book)
    {
        if (!await _service.Update(id, book))
        {
            return BadRequest();
        }
        return Ok("Book updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!await _service.Delete(id))
        {
            return BadRequest();
        }
        return Ok("Book deleted");
    }
}