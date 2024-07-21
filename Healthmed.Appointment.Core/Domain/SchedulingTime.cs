using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public sealed record SchedulingTime
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public SchedulingTime(DateTime startTime, DateTime endTime)
        {
            InvalidPeriodException.ThrowIf(DateTime.Compare(startTime, endTime) >= 0);

            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
