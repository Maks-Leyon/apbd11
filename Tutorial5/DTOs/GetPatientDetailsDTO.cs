namespace Tutorial5.DTOs;

public class GetPatientDetailsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionDto> Prescriptions { get; set; }
}

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDetailsDto> Medicaments { get; set; }
    public DoctorDetailsDto Doctor { get; set; }
}

public class MedicamentDetailsDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? Dose { get; set; }
}

public class DoctorDetailsDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}