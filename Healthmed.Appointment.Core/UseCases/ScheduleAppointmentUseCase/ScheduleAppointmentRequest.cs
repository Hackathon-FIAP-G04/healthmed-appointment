namespace Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase
{
    public class ScheduleAppointmentRequest
    {
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
    }
}
