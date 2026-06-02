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

    [HttpGet]
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
        var searchedBook = _books.Find(b => b.Id == id);
        if (searchedBook == null)
        {
            return NotFound($"Book with id: {id} not found");
        }
        
        return Ok(searchedBook);
    }

    [HttpPost]
    public IActionResult Post(Book book)
    {
        var existingBook = _books.Find(b => b.Id == book.Id);
        if (existingBook != null)
        {
            return BadRequest("Book already exists");
        }
        
        _books.Add(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Book book)
    {
        var bookToUpdate = _books.Find(b => b.Id == id);
        if (bookToUpdate == null)
        {
            return NotFound($"Book with id: {id} not found");
        }
        
        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Isbn = book.Isbn;
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var bookToDelete = _books.Find(b => b.Id == id);
        if (bookToDelete == null)
        {
            return NotFound($"Book with id: {id} not found");
        }

        _books.Remove(bookToDelete);
        return NoContent();
    }
}