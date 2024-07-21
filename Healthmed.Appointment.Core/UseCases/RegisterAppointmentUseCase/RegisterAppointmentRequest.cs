namespace Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase
{
    public class RegisterAppointmentRequest
    {
        public Guid DoctorId { get; set; }
        public RegisterServicePeriodTimeRequest StartTime { get; set; }
        public RegisterServicePeriodTimeRequest EndTime { get; set; }
    }

    public record RegisterServicePeriodTimeRequest(int Hour, int Minute);
}
