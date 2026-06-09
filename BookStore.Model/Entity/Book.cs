namespace BookStore.Model.Entity;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int Pages { get; set; }
    public string Isbn { get; set; }
    public int AuthorId { get; set; }
}