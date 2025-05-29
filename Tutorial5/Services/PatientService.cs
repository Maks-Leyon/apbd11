using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Exceptions;

namespace Tutorial5.Services;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _context;

    public PatientService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<GetPatientDetailsDTO> getPatient(int patientId)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Where(p => p.IdPatient == patientId)
            .FirstOrDefaultAsync();

        if (patient == null)
        {
            throw new NotFoundException($"Pacjetn o ID {patientId} nie zanleziony.");
        }

        var prescriptionsDto = patient.Prescriptions
            .OrderByDescending(p => p.DueDate) // Sort by DueDate descending
            .Select(p => new PrescriptionDto
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDetailsDto
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Dose = pm.Dose
                }).ToList(),
                Doctor = new DoctorDetailsDto
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName
                }
            }).ToList();

        return new GetPatientDetailsDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = prescriptionsDto
        };
    }
}