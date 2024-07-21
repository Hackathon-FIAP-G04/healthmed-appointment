using Healthmed.Appointment.Core.Abstractions;

namespace Healthmed.Appointment.Core.Domain
{
    public class ServicePeriod : Entity<Id>, IAggregateRoot
    {
        public Id DoctorId { get; set; }
        public Period Period { get; private set; }
        public Duration Duration { get; set; }
        public Price Price { get; set; }

        public ServicePeriod(Id doctorId, Period period, Duration duration, Price price)
        {
            DoctorId = doctorId;
            Period = period;
            Duration = duration;
            Price = price;
        }

        public void Update(Period period, Duration duration, Price price)
        {
            Period = period;
            Duration = duration;
            Price = price;
        }
    }
}
