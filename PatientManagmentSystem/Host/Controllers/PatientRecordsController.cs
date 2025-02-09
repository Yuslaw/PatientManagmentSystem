using Microsoft.AspNetCore.Mvc;
using PatientManagmentSystem.Application.Contract;
using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Host.Controllers
{
    [ApiController]
    [Route("api/patients/{patientId}/records")]
    public class PatientRecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public PatientRecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public IActionResult GetPatientRecords(int patientId) => Ok(_recordService.GetPatientRecords(patientId));

        [HttpPost]
        public IActionResult CreatePatientRecord(int patientId, [FromBody] PatientRecord record)
        {
            try
            {
                var createdRecord = _recordService.CreatePatientRecord(patientId, record);
                return CreatedAtAction(nameof(GetPatientRecords), new { patientId = createdRecord.PatientId }, createdRecord);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatientRecord(int patientId, int id, [FromBody] PatientRecord updatedRecord)
        {
            try
            {
                _recordService.UpdatePatientRecord(patientId, id, updatedRecord);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
