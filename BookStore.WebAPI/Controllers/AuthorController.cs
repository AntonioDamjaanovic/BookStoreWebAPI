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
    public IActionResult Get(string? firstName, string? lastName)
    {
        AuthorService service = new AuthorService();
        AuthorFilter filter = new AuthorFilter(firstName, lastName);
        
        List<Author>? authors = service.GetAll(filter);
        if (authors == null)
        {
            return NotFound();
        }
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        AuthorService service = new AuthorService();
        Author? author = service.Get(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Author author)
    {
        AuthorService service = new AuthorService();
        if (!service.Add(author))
        {
            return BadRequest();
        }
        return Ok("Author added");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Author author)
    {
        AuthorService service = new AuthorService();
        if (!service.Update(id, author))
        {
            return BadRequest();
        }
        return Ok("Author updated");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        AuthorService service = new AuthorService();
        if (!service.Delete(id))
        {
            return BadRequest();
        }
        return Ok("Author deleted");
    }
}