using Microsoft.EntityFrameworkCore;
using PatientManagmentSystem.Domain.Entities;

namespace PatientManagmentSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
    }
}
