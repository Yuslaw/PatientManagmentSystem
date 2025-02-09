using PatientManagmentSystem.Domain.Entities;
using PatientManagmentSystem.Infrastructure.Data;
using PatientManagmentSystem.Infrastructure.Services;
using System;
using Xunit;
using Moq;

namespace PatientManagmentSystem.Test
{
    public class PatientServiceTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
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
            _mockContext.Verify(c => c.Add(It.IsAny<Patient>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}