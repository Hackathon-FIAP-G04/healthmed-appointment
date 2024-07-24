using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.QueryPatientAppointmentsUseCase;
using Moq;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class QueryPatientAppointmentsUseCaseTest
    {
        [Fact]
        public async Task QueryAppointments_WithValidPatientId_ShouldReturnPatientAppointments()
        {
            // Arrange
            var mockAppointmentsRepository = new Mock<IAppointmentRepository>();
            var patientId = Guid.NewGuid();
            var appointments = new List<Appointment.Core.Domain.Appointment>
        {
            new Appointment.Core.Domain.Appointment(new SchedulingPeriod(DateTime.Now, DateTime.Now.AddHours(1)), Guid.NewGuid()),
            new Appointment.Core.Domain.Appointment(new SchedulingPeriod(DateTime.Now.AddHours(1), DateTime.Now.AddHours(2)), Guid.NewGuid())
        };

            mockAppointmentsRepository.Setup(repo => repo.GetByPatient(patientId)).ReturnsAsync(appointments);

            var useCase = new QueryPatientAppointmentsUseCase(mockAppointmentsRepository.Object);

            // Act
            var result = await useCase.QueryAppointments(patientId);

            // Assert
            Assert.Equal(patientId, result.PatientId);
            Assert.Equal(appointments.Count(), result.Appointments.Count());
        }

        [Fact]
        public async Task QueryAppointments_WithNoAppointments_ShouldReturnEmptyList()
        {
            // Arrange
            var mockAppointmentsRepository = new Mock<IAppointmentRepository>();
            var patientId = Guid.NewGuid();
            var appointments = new List<Appointment.Core.Domain.Appointment>();

            mockAppointmentsRepository.Setup(repo => repo.GetByPatient(patientId)).ReturnsAsync(appointments);

            var useCase = new QueryPatientAppointmentsUseCase(mockAppointmentsRepository.Object);

            // Act
            var result = await useCase.QueryAppointments(patientId);

            // Assert
            Assert.Equal(patientId, result.PatientId);
            Assert.Empty(result.Appointments);
        }
    }
}
