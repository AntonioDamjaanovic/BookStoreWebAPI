using System.Text;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Store.WebAPI.Models;

namespace Store.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    
    public AuthorController(ILogger<AuthorController> logger)
    {
        _logger = logger;
    }
    
    private string connectionString = "Host=localhost;" +
                                      "Port=5432;" +
                                      "Username=postgres;" +
                                      "Password=123456;" +
                                      "Database=BooksDatabase";

    [HttpGet]
    public IActionResult Get(string? firstName, string? lastName)
    {
        try
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            
            StringBuilder commandText = new StringBuilder();
            commandText.Append(
                "SELECT a.\"Id\", a.\"FirstName\", a.\"LastName\", a.\"BirthDate\", a.\"DeathDate\", " +
                "b.\"Id\", b.\"Title\", b.\"Description\", b.\"Genre\", b.\"Pages\", b.\"Isbn\", b.\"AuthorId\" " +
                "FROM \"Author\" a " +
                "LEFT JOIN \"Book\" b ON a.\"Id\" = b.\"AuthorId\" " +
                "WHERE 1 = 1");
            
            using NpgsqlCommand command = new NpgsqlCommand();
            
            if (firstName != null)
            {
                commandText.Append(" AND \"FirstName\" = @FirstName");
                command.Parameters.AddWithValue("FirstName", firstName);
            }

            if (lastName != null)
            {
                commandText.Append(" AND \"LastName\" = @LastName");
                command.Parameters.AddWithValue("LastName", lastName);
            }
            
            command.CommandText = commandText.ToString();
            command.Connection = connection;
            
            connection.Open();
            var reader = command.ExecuteReader();

            var authorsById = new Dictionary<int, Author>();
            while (reader.Read())
            {
                int authorId = reader.GetFieldValue<int>(0);

                if (!authorsById.TryGetValue(authorId, out Author? author))
                {
                    author = new Author();
                    author.Id = authorId;
                    author.FirstName = reader.GetFieldValue<string>(1);
                    author.LastName = reader.GetFieldValue<string>(2);
                    author.BirthDate = reader.GetFieldValue<DateTime>(3);
                    author.DeathDate = reader.IsDBNull(4) ? null : reader.GetFieldValue<DateTime>(4);
                    
                    authorsById.Add(authorId, author);
                }

                if (!reader.IsDBNull(5))
                {
                    Book book = new Book();
                    book.Id = reader.GetFieldValue<int>(5);
                    book.Title = reader.GetFieldValue<string>(6);
                    book.Description = reader.GetFieldValue<string>(7);
                    book.Genre = reader.GetFieldValue<string>(8);
                    book.Pages = reader.GetFieldValue<int>(9);
                    book.Isbn = reader.GetFieldValue<string>(10);
                    book.AuthorId = reader.GetFieldValue<int>(11);
                    
                    author.Books.Add(book);
                }
            }
            
            connection.Close();
            
            List<Author> authors = authorsById.Values.ToList();
            if (authors.Count == 0)
            {
                return NotFound("No authors found");
            }
            return Ok(authors);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            Author author = new Author();
            
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "SELECT * FROM \"Author\" WHERE \"Id\" = @Id";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            connection.Open();
            
            var reader = command.ExecuteReader();
            reader.Read();
            
            author.Id = reader.GetFieldValue<int>(0);
            author.FirstName = reader.GetFieldValue<string>(1);
            author.LastName = reader.GetFieldValue<string>(2);
            author.BirthDate = reader.GetFieldValue<DateTime>(3);
            author.DeathDate = reader.IsDBNull(4) ? null : reader.GetFieldValue<DateTime>(4);
            
            connection.Close();
            return Ok(author);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Author author)
    {
        try
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "INSERT INTO \"Author\" (\"FirstName\", \"LastName\", \"BirthDate\", \"DeathDate\") " +
                "VALUES (@FirstName, @LastName, @BirthDate, @DeathDate)";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("FirstName", author.FirstName);
            command.Parameters.AddWithValue("LastName", author.LastName);
            command.Parameters.AddWithValue("BirthDate", author.BirthDate);
            command.Parameters.AddWithValue("DeathDate", author.DeathDate != null ? (object)author.DeathDate : DBNull.Value);
            
            connection.Open();
            int rowsChanged = command.ExecuteNonQuery();
            connection.Close();
            
            return Ok(rowsChanged);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Author author)
    {
        try
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "UPDATE \"Author\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"BirthDate\" = @BirthDate, \"DeathDate\" = @DeathDate WHERE \"Id\" = @Id";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("Id", id);
            command.Parameters.AddWithValue("FirstName", author.FirstName);
            command.Parameters.AddWithValue("LastName", author.LastName);
            command.Parameters.AddWithValue("BirthDate", author.BirthDate);
            command.Parameters.AddWithValue("DeathDate", author.DeathDate != null ? (object)author.DeathDate : DBNull.Value);

            connection.Open();
            int rowsChanged = command.ExecuteNonQuery();
            connection.Close();

            return Ok(rowsChanged);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "DELETE FROM \"Author\" WHERE \"Id\" = @Id";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            
            connection.Open();
            int rowsChanged = command.ExecuteNonQuery();
            connection.Close();
            
            return Ok(rowsChanged);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}