using System.ComponentModel.DataAnnotations;

namespace Tutorial5.DTOs;

public class AddPrescriptionRequestDto
{
    [Required]
    public PatientDto Patient { get; set; }
        
    [Required]
    public DoctorDto Doctor { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public ICollection<MedicamentDto> Medicaments { get; set; }
}

public class PatientDto
{
    public int? IdPatient { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }
}

public class MedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}

public class DoctorDto
{
    [Required]
    public int IdDoctor { get; set; }
}