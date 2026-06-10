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
    private IMapper _mapper;

    public BookController(IBookService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? genre)
    {
        BookFilter filter = new BookFilter(genre);
        
        List<Book>? books = await _service.GetAllAsync(filter);
        List<BookDto> bookDtos = new List<BookDto>();
        
        if (books != null)
        {
            foreach (var book in books)
            {
                BookDto bookDto = _mapper.Map<BookDto>(book);
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
        if (book != null)
        {
            BookDto bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        
        if (!await _service.AddAsync(book))
        {
            return BadRequest();
        }
        return Ok("Book added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        
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