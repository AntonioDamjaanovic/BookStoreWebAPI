namespace Store.WebAPI.Models;

public class DoctorPatient
{
    public required int DoctorId { get; set; }
    public required int PatientId { get; set; }
    public string? Condition { get; set; }
    public string? Treatment  { get; set; }
    public required DateTime AdmissionTime { get; set; }
    public DateTime? ReleaseTime { get; set; }
}