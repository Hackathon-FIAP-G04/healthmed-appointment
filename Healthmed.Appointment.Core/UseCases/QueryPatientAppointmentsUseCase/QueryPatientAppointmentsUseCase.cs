using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.QueryPatientAppointmentsUseCase
{
    public interface IQueryPatientAppointmentsUseCase
    {
        Task<QueryPatientAppointmentsResponse> QueryAppointments(Guid patientId);
    }

    public class QueryPatientAppointmentsUseCase : IQueryPatientAppointmentsUseCase
    {
        private readonly IAppointmentRepository _appointmentsRepository;

        public QueryPatientAppointmentsUseCase(IAppointmentRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<QueryPatientAppointmentsResponse> QueryAppointments(Guid patientId)
        {
            var result = await _appointmentsRepository.GetByPatient(patientId);

            return new QueryPatientAppointmentsResponse(result, patientId);
        }
    }
}
