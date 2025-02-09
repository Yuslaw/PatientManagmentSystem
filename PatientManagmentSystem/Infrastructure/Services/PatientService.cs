using PatientManagmentSystem.Application.Contract;
using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Data;
using System;

namespace PatientManagmentSystem.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetPatients() => _context.Patients.Where(p => !p.IsDeleted);

        public Patient GetPatient(int id) => _context.Patients.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

        public Patient CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient;
        }

        public void UpdatePatient(int id, Patient updatedPatient)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (patient == null) throw new Exception("Patient not found");

            patient.Name = updatedPatient.Name;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            _context.SaveChanges();
        }

        public void SoftDeletePatient(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null) throw new Exception("Patient not found");

            patient.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
