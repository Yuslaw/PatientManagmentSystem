using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Application.Contract
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetPatients();
        Patient GetPatient(int id);
        Patient CreatePatient(Patient patient);
        void UpdatePatient(int id, Patient patient);
        void SoftDeletePatient(int id);
    }
}
