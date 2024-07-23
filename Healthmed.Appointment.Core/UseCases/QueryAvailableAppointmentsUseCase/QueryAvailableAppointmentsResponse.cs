namespace Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase
{
    public class QueryAvailableAppointmentsResponse
    {
        public Guid DoctorId { get; set; }
        public IEnumerable<QueryAvailableAppointmentsResponseItem> AvailableAppointments { get; set; }

        public QueryAvailableAppointmentsResponse(IEnumerable<Domain.Appointment> appointments, Guid doctorId)
        {
            DoctorId = doctorId;
            AvailableAppointments = appointments.Select(x => new QueryAvailableAppointmentsResponseItem(x)).ToList();
        }
    }

    public class QueryAvailableAppointmentsResponseItem
    {
        public Guid AppointmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public QueryAvailableAppointmentsResponseItem(Domain.Appointment appointment)
        {
            AppointmentId = appointment.Id;
            StartTime = appointment.Period.StartTime;
            EndTime = appointment.Period.EndTime;
        }
    }
}
