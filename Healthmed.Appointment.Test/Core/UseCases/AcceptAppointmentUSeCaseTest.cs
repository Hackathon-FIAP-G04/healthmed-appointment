using FluentAssertions;
using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.AcceptAppointmentUseCase;
using Moq;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class AcceptAppointmentUSeCaseTest
    {
        [Fact]
        public async Task AcceptAppointment_WithValidAppointmentId_ShouldAcceptAppointment()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            var appointment = new Appointment.Core.Domain.Appointment(new(DateTime.Now, DateTime.Now.AddHours(1)), Guid.NewGuid());
            appointment.Schedule(Guid.NewGuid());
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync(appointment);

            var useCase = new AcceptAppointmentUseCase(mockRepository.Object);

            // Act
            await useCase.AcceptAppointment(appointmentId);

            // Assert
            appointment.Status.Should().Be(AppointmentStatus.Accepted);
            mockRepository.Verify(repo => repo.Update(appointment), Times.Once);
        }

        [Fact]
        public async Task AcceptAppointment_WithInvalidAppointmentId_ShouldThrowAppointmentNotFoundException()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync((Appointment.Core.Domain.Appointment)null);

            var useCase = new AcceptAppointmentUseCase(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<AppointmentNotFoundException>(() => useCase.AcceptAppointment(appointmentId));
            mockRepository.Verify(repo => repo.Update(It.IsAny<Appointment.Core.Domain.Appointment>()), Times.Never);
        }
    }
}
