using Microsoft.AspNetCore.Mvc;
using PatientManagmentSystem.Application.Contract;
using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult GetPatients() => Ok(_patientService.GetPatients());

        [HttpGet("{id}")]
        public IActionResult GetPatient(int id)
        {
            var patient = _patientService.GetPatient(id);
            return patient == null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] Patient patient)
        {
            var createdPatient = _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patient updatedPatient)
        {
            try
            {
                _patientService.UpdatePatient(id, updatedPatient);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult SoftDeletePatient(int id)
        {
            try
            {
                _patientService.SoftDeletePatient(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
