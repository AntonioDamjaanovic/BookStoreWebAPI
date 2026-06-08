namespace BookStore.Common;

public class BookFilter
{ 
    public string? Genre  { get; set; }
    
    public BookFilter(string? genre)
    {
        Genre = genre;
    }
}