using Healthmed.Appointment.Core.Abstractions;

namespace Healthmed.Appointment.Core.Domain
{
    public abstract class AppointmentDomainEvent : IDomainEvent
    {
        public Id DoctorId { get; set; }
        public Id PatientId { get; set; }
        public Id AppointmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public AppointmentDomainEvent(Id doctorId, Id patientId, Id appointmentId, DateTime startTime, DateTime endTime)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentId = appointmentId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
