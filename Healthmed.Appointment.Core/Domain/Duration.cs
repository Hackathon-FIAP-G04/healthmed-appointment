using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public sealed record Duration
    {
        public int Minutes { get; }

        public Duration(int minutes)
        {
            InvalidDurationException.ThrowIf(minutes <= 0);

            Minutes = minutes;
        }

        public override string ToString() => Minutes.ToString();

        public static implicit operator int(Duration duration) => duration.Minutes;

        public static implicit operator Duration(int minutes) => new Duration(minutes);
    }
}
