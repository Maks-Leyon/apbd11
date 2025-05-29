using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace Tutorial5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    readonly IPatientService _patientService;
    

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }


    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetPatient(int patientId)
    {
        var res = await _patientService.getPatient(patientId);
        return Ok(res);
    }
}