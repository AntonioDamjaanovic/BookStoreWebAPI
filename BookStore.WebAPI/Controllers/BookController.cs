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
    public IActionResult Get(string? genre)
    {
        BookService service = new BookService();
        BookFilter filter = new BookFilter(genre);
        
        List<Book>? books = service.GetAll(filter);
        if (books == null)
        {
            return NotFound();
        }
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        BookService service = new BookService();
        Book? book = service.Get(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Book book)
    {
        BookService service = new BookService();
        if (!service.Add(book))
        {
            return BadRequest();
        }
        return Ok("Book added");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Book book)
    {
        BookService service = new BookService();
        if (!service.Update(id, book))
        {
            return BadRequest();
        }
        return Ok("Book updated");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        BookService service = new BookService();
        if (!service.Delete(id))
        {
            return BadRequest();
        }
        return Ok("Book deleted");
    }
}