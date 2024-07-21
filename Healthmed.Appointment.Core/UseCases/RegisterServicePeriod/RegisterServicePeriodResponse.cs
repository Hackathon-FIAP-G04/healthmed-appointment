using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.RegisterServicePeriod
{
    public class RegisterServicePeriodResponse : RegisterServicePeriodRequest
    {
        public RegisterServicePeriodResponse(ServicePeriod servicePeriod)
        {
            DoctorId = servicePeriod.Id;
            StartTime = new(servicePeriod.Period.StartHour, servicePeriod.Period.StartMinute);
            EndTime = new(servicePeriod.Period.EndHour, servicePeriod.Period.EndMinute);
            Duration = servicePeriod.Duration;
            Price = servicePeriod.Price;
        }
    }
}
