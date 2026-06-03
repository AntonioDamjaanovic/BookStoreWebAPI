namespace Store.WebAPI.Models;

public class Doctor
{
    public required int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public Specialiazation? Specialiazation { get; set; }
    public Hospital? Hospital { get; set; }
}