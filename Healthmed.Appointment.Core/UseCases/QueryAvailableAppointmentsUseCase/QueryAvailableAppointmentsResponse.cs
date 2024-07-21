using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase
{
    public class QueryAvailableAppointmentsResponse
    {
        public Guid DoctorId { get; set; }
        public IEnumerable<QueryAvailableAppointmentsResponseItem> AvailableAppointments { get; set; }

        public QueryAvailableAppointmentsResponse(IEnumerable<Domain.Appointment> appointments)
        {
            DoctorId = appointments.FirstOrDefault()?.DoctorId;
            AvailableAppointments = appointments.Select(x => new QueryAvailableAppointmentsResponseItem(x)).ToList();
        }
    }

    public class QueryAvailableAppointmentsResponseItem
    {
        public QueryAvailableAppointmentsResponsePeriod StartTime { get; set; }
        public QueryAvailableAppointmentsResponsePeriod EndTime { get; set; }

        public QueryAvailableAppointmentsResponseItem(Domain.Appointment appointment)
        {
            StartTime = new(appointment.Period.StartHour, appointment.Period.StartMinute);
            EndTime = new(appointment.Period.EndHour, appointment.Period.EndMinute);
        }
    }

    public record QueryAvailableAppointmentsResponsePeriod(int hour, int minute);
}
