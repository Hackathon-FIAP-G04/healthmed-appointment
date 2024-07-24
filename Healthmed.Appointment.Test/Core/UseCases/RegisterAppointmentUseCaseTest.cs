using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase;
using Moq;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class RegisterAppointmentUseCaseTest
    {
        [Fact]
        public async Task RegisterAppointment_WithNoConflictingAppointment_ShouldSaveAppointment()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var doctorId = Guid.NewGuid();
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(1);
            var period = new SchedulingPeriod(startTime, endTime);

            var request = new RegisterAppointmentRequest
            {
                DoctorId = doctorId,
                StartTime = startTime,
                EndTime = endTime
            };

            mockRepository.Setup(repo => repo.Exists(doctorId, period)).ReturnsAsync((Appointment.Core.Domain.Appointment)null);

            var useCase = new RegisterAppointmentUseCase(mockRepository.Object);

            // Act
            var result = await useCase.RegisterAppointment(request);

            // Assert
            var expectedAppointment = new Appointment.Core.Domain.Appointment(period, doctorId);
            mockRepository.Verify(repo => repo.Save(It.IsAny<Appointment.Core.Domain.Appointment>()), Times.Once);
            Assert.Equal(expectedAppointment.DoctorId.Value, result.DoctorId);
        }

        [Fact]
        public async Task RegisterAppointment_WithConflictingAppointment_ShouldThrowConflictingAppointmentException()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var doctorId = Guid.NewGuid();
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(1);
            var period = new SchedulingPeriod(startTime, endTime);

            var request = new RegisterAppointmentRequest
            {
                DoctorId = doctorId,
                StartTime = startTime,
                EndTime = endTime
            };

            var existingAppointment = new Appointment.Core.Domain.Appointment(period, doctorId);
            mockRepository.Setup(repo => repo.Exists(doctorId, period)).ReturnsAsync(existingAppointment);

            var useCase = new RegisterAppointmentUseCase(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ConflictingAppointmentException>(() => useCase.RegisterAppointment(request));
            mockRepository.Verify(repo => repo.Save(It.IsAny<Appointment.Core.Domain.Appointment>()), Times.Never);
        }
    }
}
