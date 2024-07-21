using Healthmed.Appointment.Core.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Core
{
    [ExcludeFromCodeCoverage]
    public static class Exceptions
    {
        public class UnavailableAppointmentException() : DomainException<UnavailableAppointmentException>("The requested appointment is not available");

        public class InvalidPeriodException() : DomainException<InvalidPeriodException>("Invalid period, the starting time must be before the end time");

        public class InvalidHourMinuteException() : DomainException<InvalidHourMinuteException>("Invalid value for hour or minute provided. Hour must be a number between 0 and 23 and minute must be a number between 0 and 59");

        public class AppointmentNotFoundException() : DomainException<AppointmentNotFoundException>("Requested appointment wasn't found");

        public class InvalidDurationException() : DomainException<InvalidDurationException>("Duration must be greater than 0 minutes");

        public class AppointmentNotAcceptableException() : DomainException<AppointmentNotAcceptableException>("Only scheduled appointments can be accepted");
        public class AppointmentNotRefusableException() : DomainException<AppointmentNotRefusableException>("Only scheduled appointments can be refused");

        public class ConflictingAppointmentException() : DomainException<ConflictingAppointmentException>("Appointment conflicts with already existing appointment");

        public class InvalidPriceException() : DomainException<InvalidPriceException>("The price amount must be equal or greater than 0");

    }
}
