namespace BookStore.Common;

public class AuthorFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public AuthorFilter(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}