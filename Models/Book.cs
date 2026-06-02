namespace Store.WebAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Isbn { get; set; }

    public Book(int id, string title, string description, string isbn)
    {
        Id = id;
        Title = title;
        Description = description;
        Isbn = isbn;
    }
}