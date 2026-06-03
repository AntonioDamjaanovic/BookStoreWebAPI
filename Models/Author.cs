namespace Store.WebAPI.Models;

public class Author
{
    public required int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public List<Book>? Books { get; set; }
}