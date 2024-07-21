namespace Healthmed.Appointment.Core.UseCases.RegisterServicePeriod
{
    public class RegisterServicePeriodRequest
    {
        public Guid DoctorId { get; set; }
        public RegisterServicePeriodTimeRequest StartTime { get; set; }
        public RegisterServicePeriodTimeRequest EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

    }

    public record RegisterServicePeriodTimeRequest(int Hour, int Minute);
}
