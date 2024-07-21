namespace Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase
{
    public class RegisterAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public RegisterAppointmentResponse(Domain.Appointment appointment)
        {
            Id = appointment.Id;
            DoctorId = appointment.DoctorId;
            StartTime = appointment.Period.StartTime;
            EndTime = appointment.Period.EndTime;
        }
    }
}
