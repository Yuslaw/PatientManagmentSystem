using Moq;
using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using PatientManagmentSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagmentSystem.Test
{
    public class RecordServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly RecordService _service;
        private readonly Mock<DbSet<PatientRecord>> _mockPatientRecordsDbSet;
        private readonly List<PatientRecord> _patientRecords;

        public RecordServiceTests()
        {
       
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<ApplicationDbContext>(options);
            _patientRecords = new List<PatientRecord>
            {
                new PatientRecord { Id = 1, PatientId = 1, Description = "Initial Checkup" },
                new PatientRecord { Id = 2, PatientId = 2, Description = "Follow-up" }
            };

            var mockPatientRecords = _patientRecords.AsQueryable();
            _mockPatientRecordsDbSet = new Mock<DbSet<PatientRecord>>();

            
            _mockPatientRecordsDbSet.As<IQueryable<PatientRecord>>().Setup(m => m.Provider).Returns(mockPatientRecords.Provider);
            _mockPatientRecordsDbSet.As<IQueryable<PatientRecord>>().Setup(m => m.Expression).Returns(mockPatientRecords.Expression);
            _mockPatientRecordsDbSet.As<IQueryable<PatientRecord>>().Setup(m => m.ElementType).Returns(mockPatientRecords.ElementType);
            _mockPatientRecordsDbSet.As<IQueryable<PatientRecord>>().Setup(m => m.GetEnumerator()).Returns(mockPatientRecords.GetEnumerator());

            
            _mockPatientRecordsDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => _patientRecords.FirstOrDefault(r => r.Id == (int)ids[0]));
            _mockContext.Setup(c => c.PatientRecords).Returns(_mockPatientRecordsDbSet.Object);
            _service = new RecordService(_mockContext.Object);
        }

        [Fact]
        public void CreatePatientRecord_ShouldAddRecordForPatient()
        {
            // Arrange
            var record = new PatientRecord { PatientId = 1, Description = "Initial Checkup" };

            // Act
            _service.CreatePatientRecord(1, record);

            // Assert
            _mockPatientRecordsDbSet.Verify(c => c.Add(It.Is<PatientRecord>(r => r.Description == record.Description)), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetPatientRecords_ShouldReturnRecordsForPatient()
        {
            // Act
            var result = _service.GetPatientRecords(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Initial Checkup", result.First().Description);
        }

        [Fact]
        public void UpdatePatientRecord_ShouldUpdateRecordDetails()
        {
            // Arrange
            var updatedRecord = new PatientRecord { Id = 1, PatientId = 1, Description = "Updated Checkup" };

            // Act
            _service.UpdatePatientRecord(1, 1, updatedRecord);

            // Assert
            var record = _patientRecords.First(r => r.Id == 1);
            Assert.Equal("Updated Checkup", record.Description);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}