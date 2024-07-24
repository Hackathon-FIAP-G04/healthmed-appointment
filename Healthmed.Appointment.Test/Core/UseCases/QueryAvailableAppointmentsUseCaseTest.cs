using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase;
using Moq;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class QueryAvailableAppointmentsUseCaseTest
    {
        [Fact]
        public async Task QueryAppointments_WithValidDoctorId_ShouldReturnAvailableAppointments()
        {
            // Arrange
            var mockAppointmentsRepository = new Mock<IAppointmentRepository>();
            var mockServicePeriodRepository = new Mock<IServicePeriodRepository>();
            var doctorId = Guid.NewGuid();
            var appointments = new List<Appointment.Core.Domain.Appointment>
            {
                new Appointment.Core.Domain.Appointment(new SchedulingPeriod(DateTime.Now, DateTime.Now.AddHours(1)), doctorId),
                new Appointment.Core.Domain.Appointment(new SchedulingPeriod(DateTime.Now.AddHours(1), DateTime.Now.AddHours(2)), doctorId)
            };
            var servicePeriod = new ServicePeriod(doctorId, new(9, 0, 18, 0), 50, 100);

            mockAppointmentsRepository.Setup(repo => repo.GetAvailablesByDoctor(doctorId)).ReturnsAsync(appointments);
            mockServicePeriodRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync(servicePeriod);

            var useCase = new QueryAvailableAppointmentsUseCase(mockAppointmentsRepository.Object, mockServicePeriodRepository.Object);

            // Act
            var result = await useCase.QueryAppointments(doctorId);

            // Assert
            Assert.Equal(doctorId, result.DoctorId);
            Assert.Equal(appointments.Count(), result.AvailableAppointments.Count());
        }

        [Fact]
        public async Task QueryAppointments_WithNoAvailableAppointments_ShouldReturnEmptyList()
        {
            // Arrange
            var mockAppointmentsRepository = new Mock<IAppointmentRepository>();
            var mockServicePeriodRepository = new Mock<IServicePeriodRepository>();
            var doctorId = Guid.NewGuid();
            var appointments = new List<Appointment.Core.Domain.Appointment>();
            var servicePeriod = new ServicePeriod(doctorId, new(9, 0, 18, 0), 50, 100);

            mockAppointmentsRepository.Setup(repo => repo.GetAvailablesByDoctor(doctorId)).ReturnsAsync(appointments);
            mockServicePeriodRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync(servicePeriod);

            var useCase = new QueryAvailableAppointmentsUseCase(mockAppointmentsRepository.Object, mockServicePeriodRepository.Object);

            // Act
            var result = await useCase.QueryAppointments(doctorId);

            // Assert
            Assert.Equal(doctorId, result.DoctorId);
            Assert.Empty(result.AvailableAppointments);
        }
    }
}
