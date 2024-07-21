using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase
{
    public class RegisterAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }

        public RegisterAppointmentResponse(Domain.Appointment appointment)
        {
            Id = appointment.Id;
            DoctorId = appointment.DoctorId;
            StartHour = appointment.Period.StartHour;
            StartMinute = appointment.Period.StartMinute;
            EndHour = appointment.Period.EndHour;
            EndMinute = appointment.Period.EndMinute;
        }
    }
}
