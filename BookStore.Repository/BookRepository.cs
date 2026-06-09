using System.Text;
using BookStore.Model.Entity;
using BookStore.Repository.Common;
using BookStore.Common;
using Npgsql;

namespace BookStore.Repository;

public class BookRepository : IBookRepository
{
    private string connectionString = "Host=localhost;" +
                                      "Port=5432;" +
                                      "Username=postgres;" +
                                      "Password=123456;" +
                                      "Database=BooksDatabase";

    public async Task<List<Book>?> GetAllAsync(BookFilter? filter)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            StringBuilder commandText = new StringBuilder();
            commandText.Append(
                "SELECT * FROM \"Book\" WHERE 1 = 1"
            );
            
            await using NpgsqlCommand command = new NpgsqlCommand(commandText.ToString(), connection);

            if (filter != null && filter.Genre != null)
            {
                commandText.Append(" AND \"Genre\" = @Genre");
                command.Parameters.Add(new NpgsqlParameter("@Genre", filter.Genre));
            }
            
            command.CommandText = commandText.ToString();
            command.Connection = connection;
            
            connection.Open();
            await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            List<Book> books = new List<Book>();
            while (await reader.ReadAsync())
            {
                Book book = new Book();
                book.Id = reader.GetFieldValue<int>(0);
                book.Title = reader.GetFieldValue<string>(1);
                book.Description = reader.GetFieldValue<string>(2);
                book.Genre = reader.GetFieldValue<string>(3);
                book.Pages = reader.GetFieldValue<int>(4);
                book.Isbn = reader.GetFieldValue<string>(5);
                book.AuthorId = reader.GetFieldValue<int>(6);
                
                books.Add(book);
            }
            
            connection.Close();
            
            return books;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Book?> GetAsync(int id)
    {
        try
        {
            Book book = new Book();
            
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = "SELECT * FROM \"Book\" WHERE \"Id\" = @Id";
            
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            connection.Open();
            
            await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            
            book.Id = reader.GetFieldValue<int>(0);
            book.Title = reader.GetFieldValue<string>(1);
            book.Description = reader.GetFieldValue<string>(2);
            book.Genre = reader.GetFieldValue<string>(3);
            book.Pages = reader.GetFieldValue<int>(4);
            book.Isbn = reader.GetFieldValue<string>(5);
            book.AuthorId = reader.GetFieldValue<int>(6);
            
            connection.Close();
            
            return book;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> SaveAsync(Book book)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "INSERT INTO \"Book\" (\"Title\", \"Description\", \"Genre\", \"Pages\", \"Isbn\", \"AuthorId\") " +
                "VALUES (@Title, @Description, @Genre, @Pages, @Isbn, @AuthorId)";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Title", book.Title);
            command.Parameters.AddWithValue("Description", book.Description);
            command.Parameters.AddWithValue("Genre", book.Genre);
            command.Parameters.AddWithValue("Pages", book.Pages);
            command.Parameters.AddWithValue("Isbn", book.Isbn);
            command.Parameters.AddWithValue("AuthorId", book.AuthorId);
            
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
    
    public async Task<bool> UpdateAsync(int id, Book book)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "UPDATE \"Book\" SET \"Title\" = @Title, \"Description\" = @Description, \"Genre\" = @Genre, \"Pages\" = @Pages, \"Isbn\" = @Isbn, \"AuthorId\" = @AuthorId " +
                "WHERE \"Id\" = @Id";
            await using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            
            command.Parameters.AddWithValue("Id", id);
            command.Parameters.AddWithValue("Title", book.Title);
            command.Parameters.AddWithValue("Description", book.Description);
            command.Parameters.AddWithValue("Genre", book.Genre);
            command.Parameters.AddWithValue("Pages", book.Pages);
            command.Parameters.AddWithValue("Isbn", book.Isbn);
            command.Parameters.AddWithValue("AuthorId", book.AuthorId);
            
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
                "DELETE FROM \"Book\" WHERE \"Id\" = @Id";
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