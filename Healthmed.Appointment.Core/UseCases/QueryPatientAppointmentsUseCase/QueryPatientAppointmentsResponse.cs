namespace Healthmed.Appointment.Core.UseCases.QueryPatientAppointmentsUseCase
{
    public class QueryPatientAppointmentsResponse
    {
        public Guid PatientId { get; set; }
        public IEnumerable<QueryPatientAppointmentsResponseItem> Appointments { get; set; }

        public QueryPatientAppointmentsResponse(IEnumerable<Core.Domain.Appointment> appointments, Guid patientId)
        {
            PatientId = patientId;
            Appointments = appointments
                .Select(x => 
                    new QueryPatientAppointmentsResponseItem(
                        x.DoctorId, 
                        x.Status.ToString(), 
                        x.Period.StartTime, 
                        x.Period.EndTime))
                .ToList();
        }
    }

    public record QueryPatientAppointmentsResponseItem(Guid DoctorId, string Status, DateTime StartTime, DateTime EndTime);

}
