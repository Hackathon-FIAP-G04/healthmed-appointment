namespace Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase
{
    public class RegisterAppointmentRequest
    {
        public Guid DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
