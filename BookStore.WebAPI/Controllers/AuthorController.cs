using BookStore.Common;
using Microsoft.AspNetCore.Mvc;
using BookStore.Model;
using BookStore.Service.Common;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private IAuthorService _service;
    
    public AuthorController(IAuthorService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? firstName, string? lastName)
    {
        AuthorFilter filter = new AuthorFilter(firstName, lastName);
        
        List<Author>? authors = await _service.GetAllAsync(filter);
        if (authors == null)
        {
            return NotFound();
        }
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Author? author = await _service.GetAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Author author)
    {
        if (!await _service.AddAsync(author))
        {
            return BadRequest();
        }
        return Ok("Author added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Author author)
    {
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