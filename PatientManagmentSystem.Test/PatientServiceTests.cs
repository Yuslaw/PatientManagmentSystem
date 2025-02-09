using Moq;
using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using PatientManagmentSystem.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagmentSystem.Test
{
    public class PatientServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly PatientService _service;
        private readonly Mock<DbSet<Patient>> _mockPatientsDbSet;
        private readonly List<Patient> _patients;

        public PatientServiceTests()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<ApplicationDbContext>(options);

            
            _patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "John Doe", IsDeleted = false },
                new Patient { Id = 2, Name = "Jane Doe", IsDeleted = true }
            };

            var mockPatients = _patients.AsQueryable();
            _mockPatientsDbSet = new Mock<DbSet<Patient>>();

            
            _mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(mockPatients.Provider);
            _mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(mockPatients.Expression);
            _mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(mockPatients.ElementType);
            _mockPatientsDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(mockPatients.GetEnumerator());

            
            _mockPatientsDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns((object[] ids) => _patients.FirstOrDefault(p => p.Id == (int)ids[0]));
            _mockContext.Setup(c => c.Patients).Returns(_mockPatientsDbSet.Object);

            _service = new PatientService(_mockContext.Object);
        }

        [Fact]
        public void CreatePatient_ShouldAddPatientToDatabase()
        {
            // Arrange
            var patient = new Patient { Name = "John Doe", DateOfBirth = DateTime.Now.AddYears(-30) };

            // Act
            _service.CreatePatient(patient);

            // Assert
            _mockPatientsDbSet.Verify(c => c.Add(It.Is<Patient>(p => p == patient)), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetPatients_ShouldReturnNonDeletedPatients()
        {
            // Act
            var result = _service.GetPatients();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().Name);
        }

        [Fact]
        public void GetPatient_ShouldReturnPatientById()
        {
            // Act
            var result = _service.GetPatient(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public void UpdatePatient_ShouldUpdatePatientDetails()
        {
            // Arrange
            var updatedPatient = new Patient { Id = 1, Name = "John Updated", DateOfBirth = DateTime.Now.AddYears(-25) };

            // Act
            _service.UpdatePatient(1, updatedPatient);

            // Assert
            var patient = _patients.First(p => p.Id == 1);
            Assert.Equal("John Updated", patient.Name);
            Assert.Equal(updatedPatient.DateOfBirth, patient.DateOfBirth);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void SoftDeletePatient_ShouldMarkPatientAsDeleted()
        {
            // Act
            _service.SoftDeletePatient(1);

            // Assert
            var patient = _patients.First(p => p.Id == 1);
            Assert.True(patient.IsDeleted);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}