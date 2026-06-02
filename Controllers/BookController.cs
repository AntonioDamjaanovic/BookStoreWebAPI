using Microsoft.AspNetCore.Mvc;
using Store.WebAPI.Models;

namespace Store.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private static List<Book> _books =
    [
        new Book(id: 1, title: "Dune", description: "Lisan Al Gaib takes over the space", isbn: "123456789"),
        new Book(id: 2, title: "1984", description: "Big Brother is watching", isbn: "98765432"),
        new Book(id: 3, title: "Fahrenheit 451", description: "Fireman starts fires", isbn: "615251371")
    ];
    
    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    [HttpGet("All")]
    public IActionResult Get()
    {
        if (_books.Count == 0)
        {
            return NotFound("No books found");
        }
        return Ok(_books);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var searchedBook = _books.Find(book => book.Id == id);
        if (searchedBook != null)
        {
            return Ok(searchedBook);
        }
        
        return NotFound($"Book with id: {id} not found");
    }

    [HttpPost("Add")]
    public IActionResult Post(Book book)
    {
        _books.Add(book);
        return Ok("Book added");
    }

    [HttpPut("Update/{id}")]
    public IActionResult Put(int id, Book book)
    {
        var bookToUpdate = _books.Find(book => book.Id == id);
        if (bookToUpdate != null)
        {
            _books.Remove(bookToUpdate);
            _books.Add(book);
            return Ok("Book updated");
        }
        return NotFound($"Book with id: {id} not found");
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var bookToDelete = _books.Find(book => book.Id == id);
        if (bookToDelete != null)
        {
            _books.Remove(bookToDelete);
            return Ok("Book deleted");
        }
        return NotFound($"Book with id: {id} not found");
    }
}