using Ardalis.SmartEnum;

namespace Healthmed.Appointment.Core.Domain
{
    public class AppointmentStatus : SmartEnum<AppointmentStatus>
    {
        public static readonly AppointmentStatus Created = new("Created", 0);
        public static readonly AppointmentStatus Scheduled = new("Scheduled", 1);
        public static readonly AppointmentStatus Accepted = new("Accepted", 2);
        public static readonly AppointmentStatus Refused = new("Refused", 3);
        public static readonly AppointmentStatus Cancelled = new("Cancelled", 4);

        private AppointmentStatus(string name, int value) : base(name, value) { }
    }
}
