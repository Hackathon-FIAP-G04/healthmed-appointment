using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase;
using Moq;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class ScheduleAppointmentUseCaseTest
    {
        [Fact]
        public async Task ScheduleAppointment_WithValidAppointmentId_ShouldScheduleAppointment()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            var patientId = Guid.NewGuid();
            var appointment = new Appointment.Core.Domain.Appointment(new(DateTime.Now, DateTime.Now.AddDays(1)), Guid.NewGuid());
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync(appointment);

            var request = new ScheduleAppointmentRequest
            {
                AppointmentId = appointmentId,
                PatientId = patientId
            };

            var useCase = new ScheduleAppointmentUseCase(mockRepository.Object);

            // Act
            var result = await useCase.ScheduleAppointment(request);

            // Assert
            mockRepository.Verify(repo => repo.Update(appointment), Times.Once);
            Assert.Equal(appointment.PatientId.Value, result.PatientId);
        }

        [Fact]
        public async Task ScheduleAppointment_WithInvalidAppointmentId_ShouldThrowAppointmentNotFoundException()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            var patientId = Guid.NewGuid();
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync((Appointment.Core.Domain.Appointment)null);

            var request = new ScheduleAppointmentRequest
            {
                AppointmentId = appointmentId,
                PatientId = patientId
            };

            var useCase = new ScheduleAppointmentUseCase(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<AppointmentNotFoundException>(() => useCase.ScheduleAppointment(request));
            mockRepository.Verify(repo => repo.Update(It.IsAny<Appointment.Core.Domain.Appointment>()), Times.Never);
        }
    }
}
