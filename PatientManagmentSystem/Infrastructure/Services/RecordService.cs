using PatientManagmentSystem.Application.Contract;
using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Data;

namespace PatientManagmentSystem.Infrastructure.Services
{
    public class RecordService : IRecordService
    {
        private readonly ApplicationDbContext _context;

        public RecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PatientRecord> GetPatientRecords(int patientId)
        {
            return _context.PatientRecords.Where(r => r.PatientId == patientId);
        }

        public PatientRecord CreatePatientRecord(int patientId, PatientRecord record)
        {
            record.PatientId = patientId;
            _context.PatientRecords.Add(record);
            _context.SaveChanges();
            return record;
        }

        public void UpdatePatientRecord(int patientId, int id, PatientRecord updatedRecord)
        {
            var record = _context.PatientRecords.FirstOrDefault(r => r.Id == id && r.PatientId == patientId);
            if (record == null) throw new Exception("Record not found");

            record.Description = updatedRecord.Description;
            _context.SaveChanges();
        }
    }
}
