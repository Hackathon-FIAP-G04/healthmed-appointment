namespace Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase
{
    public class ScheduleAppointmentResponse
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public ScheduleAppointmentPeriodResponse Period { get; set; }

        public ScheduleAppointmentResponse(Domain.Appointment appointment)
        {
            AppointmentId = appointment.Id;
            DoctorId = appointment.DoctorId;
            PatientId = appointment.PatientId;
            Period = new(appointment.Period.StartTime, appointment.Period.EndTime);
        }
    }

    public record ScheduleAppointmentPeriodResponse(DateTime StartTime, DateTime EndTime);
}
