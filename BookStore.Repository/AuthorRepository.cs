using System.Text;
using BookStore.Common;
using BookStore.Model;
using BookStore.Model.Entity;
using BookStore.Repository.Common;
using Npgsql;

namespace BookStore.Repository;

public class AuthorRepository : IAuthorRepository
{
    private string connectionString = "Host=localhost;" +
                                      "Port=5432;" +
                                      "Username=postgres;" +
                                      "Password=123456;" +
                                      "Database=BooksDatabase";

    public async Task<List<Author>?> GetAllAsync(AuthorFilter? filter)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            
            StringBuilder commandText = new StringBuilder();
            commandText.Append(
                "SELECT a.\"Id\", a.\"FirstName\", a.\"LastName\", a.\"BirthDate\", a.\"DeathDate\", " +
                "b.\"Id\", b.\"Title\", b.\"Description\", b.\"Genre\", b.\"Pages\", b.\"Isbn\", b.\"AuthorId\" " +
                "FROM \"Author\" a " +
                "LEFT JOIN \"Book\" b ON a.\"Id\" = b.\"AuthorId\" " +
                "WHERE 1 = 1");
            
            await using NpgsqlCommand command = new NpgsqlCommand();
            
            if (filter != null && filter.FirstName != null)
            {
                commandText.Append(" AND \"FirstName\" = @FirstName");
                command.Parameters.AddWithValue("FirstName", filter.FirstName);
            }

            if (filter != null && filter.LastName != null)
            {
                commandText.Append(" AND \"LastName\" = @LastName");
                command.Parameters.AddWithValue("LastName", filter.LastName);
            }
            
            command.CommandText = commandText.ToString();
            command.Connection = connection;
            
            connection.Open();
            await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            var authorsById = new Dictionary<int, Author>();
            while (await reader.ReadAsync())
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
            
            return authorsById.Values.ToList();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Author?> GetAsync(int id)
    {
        try
        {
            Author author = new Author();
            
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "SELECT * FROM \"Author\" WHERE \"Id\" = @Id";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            connection.Open();
            
            await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            
            author.Id = reader.GetFieldValue<int>(0);
            author.FirstName = reader.GetFieldValue<string>(1);
            author.LastName = reader.GetFieldValue<string>(2);
            author.BirthDate = reader.GetFieldValue<DateTime>(3);
            author.DeathDate = reader.IsDBNull(4) ? null : reader.GetFieldValue<DateTime>(4);
            
            connection.Close();
            
            return author;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> SaveAsync(Author author)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "INSERT INTO \"Author\" (\"FirstName\", \"LastName\", \"BirthDate\", \"DeathDate\") " +
                "VALUES (@FirstName, @LastName, @BirthDate, @DeathDate)";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("FirstName", author.FirstName);
            command.Parameters.AddWithValue("LastName", author.LastName);
            command.Parameters.AddWithValue("BirthDate", author.BirthDate);
            command.Parameters.AddWithValue("DeathDate", author.DeathDate != null ? (object)author.DeathDate : DBNull.Value);
            
            connection.Open();
            int rowsChanged = await command.ExecuteNonQueryAsync();
            connection.Close();
            
            return rowsChanged > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, Author author)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "UPDATE \"Author\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"BirthDate\" = @BirthDate, \"DeathDate\" = @DeathDate WHERE \"Id\" = @Id";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("Id", id);
            command.Parameters.AddWithValue("FirstName", author.FirstName);
            command.Parameters.AddWithValue("LastName", author.LastName);
            command.Parameters.AddWithValue("BirthDate", author.BirthDate);
            command.Parameters.AddWithValue("DeathDate", author.DeathDate != null ? (object)author.DeathDate : DBNull.Value);

            connection.Open();
            int rowsChanged = await command.ExecuteNonQueryAsync();
            connection.Close();
            
            return rowsChanged > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "DELETE FROM \"Author\" WHERE \"Id\" = @Id";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            
            connection.Open();
            int rowsChanged = await command.ExecuteNonQueryAsync();
            connection.Close();
            
            return rowsChanged > 0;
        } catch (Exception e)
        {
            return false;
        }
    }
}