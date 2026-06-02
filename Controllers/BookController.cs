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
    public IEnumerable<Book> Get()
    {
        return _books.ToArray();
    }

    [HttpGet("{id}")]
    public Book? Get(int id)
    {
        var searchedBook = _books.Find(book => book.Id == id);
        if (searchedBook != null)
        {
            return searchedBook;
        }
        
        return null;
    }

    [HttpPost]
    public bool Post(Book book)
    {
        _books.Add(book);
        return true;
    }

    [HttpPut("{id}")]
    public Boolean Put(int id, Book book)
    {
        var bookToUpdate = _books.Find(book => book.Id == id);
        if (bookToUpdate != null)
        {
            _books.Remove(bookToUpdate);
            _books.Add(book);
            return true;
        }
        return false;
    }

    [HttpDelete("{id}")]
    public Boolean Delete(int id)
    {
        var bookToDelete = _books.Find(book => book.Id == id);
        if (bookToDelete != null)
        {
            _books.Remove(bookToDelete);
            return true;
        }
        return false;
    }
}