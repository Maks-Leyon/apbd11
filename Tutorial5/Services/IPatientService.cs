﻿using Tutorial5.DTOs;

namespace Tutorial5.Services;

public interface IPatientService
{
    Task<GetPatientDetailsDTO> getPatient(int patientId);
}