using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public sealed record SchedulingPeriod
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public SchedulingPeriod(DateTime startTime, DateTime endTime)
        {
            InvalidPeriodException.ThrowIf(DateTime.Compare(startTime, endTime) >= 0);

            StartTime = startTime;
            EndTime = endTime;
        }

        public SchedulingPeriod(DateTime startTime, int minutes)
        {
            var endTime = startTime.AddMinutes(minutes);

            InvalidPeriodException.ThrowIf(DateTime.Compare(startTime, endTime) >= 0);

            StartTime = startTime;
            EndTime = endTime;
        }

        public SchedulingPeriod Next(int minutes)
            => new SchedulingPeriod(StartTime.AddMinutes(minutes), EndTime.AddMinutes(minutes));
    }
}
