using BookStore.Common;
using Microsoft.AspNetCore.Mvc;
using BookStore.Service;
using BookStore.Model;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(string? firstName, string? lastName)
    {
        AuthorService service = new AuthorService();
        AuthorFilter filter = new AuthorFilter(firstName, lastName);
        
        List<Author>? authors = await service.GetAllAsync(filter);
        if (authors == null)
        {
            return NotFound();
        }
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        AuthorService service = new AuthorService();
        Author? author = await service.GetAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Author author)
    {
        AuthorService service = new AuthorService();
        if (!await service.AddAsync(author))
        {
            return BadRequest();
        }
        return Ok("Author added");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] Author author)
    {
        AuthorService service = new AuthorService();
        if (!await service.UpdateAsync(id, author))
        {
            return BadRequest();
        }
        return Ok("Author updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        AuthorService service = new AuthorService();
        if (!await service.DeleteAsync(id))
        {
            return BadRequest();
        }
        return Ok("Author deleted");
    }
}