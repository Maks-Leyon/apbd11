﻿using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; }
}