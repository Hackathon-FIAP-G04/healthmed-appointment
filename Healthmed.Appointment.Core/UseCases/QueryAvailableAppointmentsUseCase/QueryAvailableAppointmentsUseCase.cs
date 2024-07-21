using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase
{
    public interface IQueryAvailableAppointmentsUseCase
    {
        Task<QueryAvailableAppointmentsResponse> QueryAppointments(Guid doctorId);
    }

    public class QueryAvailableAppointmentsUseCase : IQueryAvailableAppointmentsUseCase
    {
        private readonly IAppointmentRepository _appointmentsRepository;

        public QueryAvailableAppointmentsUseCase(IAppointmentRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<QueryAvailableAppointmentsResponse> QueryAppointments(Guid doctorId)
        {
            var appointments = await _appointmentsRepository.GetAvailablesByDoctor(doctorId);

            return new(appointments);
        }
    }
}
