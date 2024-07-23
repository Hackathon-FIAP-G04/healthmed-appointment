using Healthmed.Appointment.Core.Abstractions;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public class Appointment : Entity<Id>, IAggregateRoot
    {
        public SchedulingPeriod Period { get; private set; }
        public Id DoctorId { get; private set; }
        public Id? PatientId { get; set; }
        public AppointmentStatus Status { get; private set; }

        public bool Available => PatientId == null && Status == AppointmentStatus.Created;

        public Appointment(SchedulingPeriod period, Id doctorId)
        {
            Id = Id.New();
            Period = period;
            DoctorId = doctorId;
            PatientId = null;
            Status = AppointmentStatus.Created;
        }

        public void Schedule(Id patientId)
        {
            UnavailableAppointmentException.ThrowIf(!Available);

            PatientId = patientId;
            Status = AppointmentStatus.Scheduled;

        }

        public void Accept()
        {
            AppointmentNotAcceptableException.ThrowIf(Status != AppointmentStatus.Scheduled);

            Status = AppointmentStatus.Accepted;

            RaiseEvent(new AcceptedAppointmentDomainEvent(DoctorId, PatientId, Id, Period.StartTime, Period.EndTime));
        }

        public void Refuse()
        {
            AppointmentNotRefusableException.ThrowIf(Status != AppointmentStatus.Scheduled);

            Status = AppointmentStatus.Refused;
        }

        public void Cancel()
        {
            AppointmentNotCancellableException.ThrowIf(Status != AppointmentStatus.Scheduled && Status != AppointmentStatus.Accepted);
            Status = AppointmentStatus.Cancelled;
        }
    }
}
