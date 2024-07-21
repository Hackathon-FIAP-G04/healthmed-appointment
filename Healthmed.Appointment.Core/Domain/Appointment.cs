﻿using Healthmed.Appointment.Core.Abstractions;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.Domain
{
    public class Appointment : Entity<Id>, IAggregateRoot
    {
        public Period Period { get; private set; }
        public Id DoctorId { get; private set; }
        public Id? PatientId { get; set; }
        public AppointmentStatus Status { get; private set; }

        public bool Available => PatientId == null;

        public Appointment(Period period, Id doctorId)
        {
            Period = period;
            DoctorId = doctorId;
            PatientId = null;
            Status = AppointmentStatus.Created;
        }

        public void Schedule(Id patientId)
        {
            UnavailableAppointmentException.ThrowIf(PatientId != null || Status != AppointmentStatus.Created);

            PatientId = patientId;
            Status = AppointmentStatus.Scheduled;

        }

        public void Accept()
        {
            AppointmentNotAcceptableException.ThrowIf(Status != AppointmentStatus.Scheduled);

            Status = AppointmentStatus.Accepted;
        }

        public void Refuse()
        {
            AppointmentNotRefusableException.ThrowIf(Status != AppointmentStatus.Scheduled);

            Status = AppointmentStatus.Refused;
        }

        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
        }
    }
}
