namespace BookStore.Model;

public class Hospital
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public int Capacity { get; set; }
    public List<Doctor>? Doctors { get; set; }
    public List<Nurse>? Nurses { get; set; }
    public List<Patient>? Patients { get; set; }
}