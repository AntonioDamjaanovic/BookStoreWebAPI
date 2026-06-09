using AutoMapper;
using BookStore.Common;
using Microsoft.AspNetCore.Mvc;
using BookStore.Model.Entity;
using BookStore.Model.Rest;
using BookStore.Service.Common;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private IAuthorService _service;
    
    private MapperConfiguration _config = new (cfg =>
    {
        cfg.CreateMap<Author, AuthorDto>();
        cfg.CreateMap<AuthorDto, Author>();
    }, new LoggerFactory());
    
    public AuthorController(IAuthorService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? firstName, string? lastName)
    {
        AuthorFilter filter = new AuthorFilter(firstName, lastName);
        
        List<Author>? authors = await _service.GetAllAsync(filter);
        List<AuthorDto> authorDtos = new List<AuthorDto>();
        var mapper = _config.CreateMapper();
        
        if (authors != null)
        {
            foreach (Author author in authors)
            {
                AuthorDto? authorDto = mapper.Map<AuthorDto>(author);
                authorDtos.Add(authorDto);
            }
            return Ok(authorDtos);
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Author? author = await _service.GetAsync(id);
        var mapper = _config.CreateMapper();
        if (author != null)
        {
            AuthorDto authorDto = mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AuthorDto authorDto)
    {
        var mapper = _config.CreateMapper();
        Author author = mapper.Map<Author>(authorDto);
        if (!await _service.AddAsync(author))
        {
            return BadRequest();
        }
        return Ok("Author added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] AuthorDto authorDto)
    {
        var mapper = _config.CreateMapper();
        Author author = mapper.Map<Author>(authorDto);
        
        if (!await _service.UpdateAsync(id, author))
        {
            return BadRequest();
        }
        return Ok("Author updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!await _service.DeleteAsync(id))
        {
            return BadRequest();
        }
        return Ok("Author deleted");
    }
}