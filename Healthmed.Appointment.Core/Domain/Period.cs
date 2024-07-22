using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public sealed record Period
    {
        public int StartHour { get; }
        public int StartMinute { get; }
        public int EndHour { get; }
        public int EndMinute { get; }

        public Period(int startHour, int startMinute, int endHour, int endMinute)
        {
            InvalidHourMinuteException.ThrowIf
            (
                startHour < 0 || startHour > 23 || endHour < 0 || endHour > 23 ||
                startMinute < 0 || startMinute > 59 || endMinute < 0 || endMinute > 59
            );

            InvalidPeriodException.ThrowIf
            (
                startHour > endHour ||
                (startHour == endHour && startMinute >= endMinute)
            );

            StartHour = startHour;
            StartMinute = startMinute;
            EndHour = endHour;
            EndMinute = endMinute;
        }

        public Period NextPeriod(int minutes)
        {
            var (startHour, startMinute) = NextPeriod(minutes, StartHour, StartMinute);
            var (endHour, endMinute) = NextPeriod(minutes, EndHour, EndMinute);

            return new Period(startHour, startMinute, endHour, endMinute);
        }

        private static (int, int) NextPeriod(int minutesAdded, int hours, int minutes)
        {
            var hoursToAdd = 0;
            int endMinutes;

            if (minutesAdded + minutes > 59)
            {
                endMinutes = (minutesAdded + minutes) % 60;
                hoursToAdd = (minutesAdded + minutes) / 60;
            }
            else
                endMinutes = minutesAdded + minutes;

            return (hours + hoursToAdd, endMinutes);
        }
    }
}
