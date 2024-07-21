using Healthmed.Appointment.Core.Domain;

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
            Period = new(appointment.Period.StartHour, appointment.Period.StartMinute, appointment.Period.EndHour, appointment.Period.EndMinute);
        }
    }

    public record ScheduleAppointmentPeriodResponse(int StartHour, int startMinute, int EndHour, int EndMinute);
}
