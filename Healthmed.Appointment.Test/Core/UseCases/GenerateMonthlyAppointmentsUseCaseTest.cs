using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.GenerateMonthlyAppointmentsUseCase;
using Moq;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class GenerateMonthlyAppointmentsUseCaseTest
    {
        [Fact]
        public async Task GenerateAppointments_WithValidDoctorId_ShouldGenerateAppointments()
        {
            // Arrange
            var mockPeriodRepository = new Mock<IServicePeriodRepository>();
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            var doctorId = Guid.NewGuid();
            var startHour = 9;
            var startMinute = 0;
            var endHour = 17;
            var endMinute = 0;
            var duration = new Duration(60);

            var servicePeriod = new ServicePeriod(
                doctorId,
                new Period(startHour, startMinute, endHour, endMinute),
                duration,
                new Price(100m)
            );

            mockPeriodRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync(servicePeriod);

            var useCase = new GenerateMonthlyAppointmentsUseCase(mockPeriodRepository.Object, mockAppointmentRepository.Object);

            // Act
            await useCase.GenerateAppointments(doctorId);

            // Assert
            mockPeriodRepository.Verify(repo => repo.GetByDoctorId(doctorId), Times.Once);

            // Note: The exact count depends on the number of possible appointment slots
            var expectedAppointmentCount = 30 * ((endHour - startHour) * 60 / duration.Minutes);
            mockAppointmentRepository.Verify(repo => repo.SaveMany(It.IsAny<List<Healthmed.Appointment.Core.Domain.Appointment>>()), Times.Once);
        }

        [Fact]
        public async Task GenerateAppointments_WithNoServicePeriod_ShouldNotGenerateAppointments()
        {
            // Arrange
            var mockPeriodRepository = new Mock<IServicePeriodRepository>();
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            var doctorId = Guid.NewGuid();

            mockPeriodRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync((ServicePeriod)null);

            var useCase = new GenerateMonthlyAppointmentsUseCase(mockPeriodRepository.Object, mockAppointmentRepository.Object);

            // Act
            await useCase.GenerateAppointments(doctorId);

            // Assert
            mockPeriodRepository.Verify(repo => repo.GetByDoctorId(doctorId), Times.Once);
            mockAppointmentRepository.Verify(repo => repo.SaveMany(It.IsAny<List<Appointment.Core.Domain.Appointment >> ()), Times.Never);
        }
    }
}
