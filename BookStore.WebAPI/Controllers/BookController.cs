using AutoMapper;
using BookStore.Common;
using BookStore.Model.Entity;
using BookStore.Model.Rest;
using BookStore.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private IBookService _service;
    private MapperConfiguration _config = new (cfg =>
    {
        cfg.CreateMap<Book, BookDto>();
        cfg.CreateMap<BookDto, Book>();
    }, new LoggerFactory());

    public BookController(IBookService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? genre)
    {
        BookFilter filter = new BookFilter(genre);
        
        List<Book>? books = await _service.GetAllAsync(filter);
        List<BookDto> bookDtos = new List<BookDto>();
        var mapper = _config.CreateMapper();
        
        if (books != null)
        {
            foreach (var book in books)
            {
                BookDto bookDto = mapper.Map<BookDto>(book);
                bookDtos.Add(bookDto);
            }
            return Ok(bookDtos);
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Book? book = await _service.GetAsync(id);
        var mapper = _config.CreateMapper();
        if (book != null)
        {
            BookDto bookDto = mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookDto bookDto)
    {
        var mapper = _config.CreateMapper();
        Book book = mapper.Map<Book>(bookDto);
        
        if (!await _service.AddAsync(book))
        {
            return BadRequest();
        }
        return Ok("Book added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] BookDto bookDto)
    {
        var mapper = _config.CreateMapper();
        Book book = mapper.Map<Book>(bookDto);
        
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