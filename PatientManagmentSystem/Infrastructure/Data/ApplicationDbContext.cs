using Microsoft.EntityFrameworkCore;
using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientRecord> PatientRecords { get; set; }
    }
}
