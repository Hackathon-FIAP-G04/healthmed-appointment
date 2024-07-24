using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.RefuseAppointmentUseCase;
using Moq;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class RefuseAppointmentUseCaseTest
    {
        [Fact]
        public async Task RefuseAppointment_WithValidAppointmentId_ShouldRefuseAppointment()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            var appointment = new Appointment.Core.Domain.Appointment(new(DateTime.Now, DateTime.Now.AddHours(1)), Guid.NewGuid());
            appointment.Schedule(Guid.NewGuid());
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync(appointment);

            var useCase = new RefuseAppointmentUseCase(mockRepository.Object);

            // Act
            await useCase.RefuseAppointment(appointmentId);

            // Assert
            mockRepository.Verify(repo => repo.Update(appointment), Times.Once);
        }

        [Fact]
        public async Task RefuseAppointment_WithInvalidAppointmentId_ShouldThrowAppointmentNotFoundException()
        {
            // Arrange
            var mockRepository = new Mock<IAppointmentRepository>();
            var appointmentId = Guid.NewGuid();
            mockRepository.Setup(repo => repo.Get(appointmentId)).ReturnsAsync((Appointment.Core.Domain.Appointment)null);

            var useCase = new RefuseAppointmentUseCase(mockRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<AppointmentNotFoundException>(() => useCase.RefuseAppointment(appointmentId));
            mockRepository.Verify(repo => repo.Update(It.IsAny<Appointment.Core.Domain.Appointment>()), Times.Never);
        }
    }
}
