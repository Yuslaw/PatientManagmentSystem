using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Application.Contract
{
    public interface IRecordService
    {
        IEnumerable<PatientRecord> GetPatientRecords(int patientId);
        PatientRecord CreatePatientRecord(int patientId, PatientRecord record);
        void UpdatePatientRecord(int patientId, int id, PatientRecord record);
    }
}
