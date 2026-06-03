namespace Store.WebAPI.Models;

public class Nurse
{
    public required int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public Hospital? Hospital { get; set; }
}