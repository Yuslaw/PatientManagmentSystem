using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Services;
using Moq;
using Microsoft.EntityFrameworkCore;
using PatientManagmentSystem.Infrastructure.Data;

namespace PatientManagmentSystem.Test
{
    public class RecordServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly RecordService _service;

        public RecordServiceTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _service = new RecordService(_mockContext.Object);
        }

        [Fact]
        public void CreatePatientRecord_ValidRecord_AddsRecord()
        {
            var record = new PatientRecord { PatientId = 1, Description = "Initial Checkup" };

            _service.CreatePatientRecord(1, record);

            _mockContext.Verify(x => x.PatientRecords.Add(It.Is<PatientRecord>(r => r.Description == record.Description)), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}