namespace Healthmed.Appointment.Core.Domain
{
    public class AcceptedAppointmentDomainEvent : AppointmentDomainEvent 
    {
        public AcceptedAppointmentDomainEvent(Id doctorId, Id patientId, Id appointmentId, DateTime startTime, DateTime endTime)
            : base(doctorId, patientId, appointmentId, startTime, endTime)
        {
            
        }
    }
}
