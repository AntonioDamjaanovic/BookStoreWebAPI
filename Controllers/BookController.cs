using Microsoft.AspNetCore.Mvc;
using Store.WebAPI.Models;

namespace Store.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    
    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get(string? genre, int? maxPages)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(Book book)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Book book)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}