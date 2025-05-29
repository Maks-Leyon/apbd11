using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Exceptions;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddNewPrescription(AddPrescriptionRequestDto presc)
    {
        if (presc.DueDate < presc.Date)
        {
            throw new ArgumentException("Data terminu nie powinna być wcześniejsza niż data wystawienia recepty.");
        }

        if (presc.Medicaments.Count > 10)
        {
            throw new ConflictException("Preskrypcja nie może zawierać więcej niż 10 leków.");
        }

        foreach (var medicamentDto in presc.Medicaments)
        {
            var medicamentExists = await _context.Medicaments.AnyAsync(m => m.IdMedicament == medicamentDto.IdMedicament);
            if (!medicamentExists)
            {
                throw new NotFoundException($"Lek o ID {medicamentDto.IdMedicament} nie znaleziony.");
            }
        }

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == presc.Doctor.IdDoctor);
        if (doctor == null)
        {
            throw new ArgumentException($"Doktor o ID {presc.Doctor.IdDoctor} nie znaleziony.");
        }

        var patient = new Patient
        {
            FirstName = presc.Patient.FirstName,
            LastName = presc.Patient.LastName,
            Birthdate = presc.Patient.Birthdate
        };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        var prescription = new Prescription
        {
            Date = presc.Date,
            DueDate = presc.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor
        };
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        foreach (var medicamentDto in presc.Medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdMedicament = medicamentDto.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Details
            };
            await _context.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
        }

        await _context.SaveChangesAsync();
    }
}