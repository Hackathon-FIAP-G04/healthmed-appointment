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
        private readonly IServicePeriodRepository _servicePeriodRepository;

        public QueryAvailableAppointmentsUseCase(IAppointmentRepository appointmentsRepository, IServicePeriodRepository servicePeriodRepository)
        {
            _appointmentsRepository = appointmentsRepository;
            _servicePeriodRepository = servicePeriodRepository;
        }

        public async Task<QueryAvailableAppointmentsResponse> QueryAppointments(Guid doctorId)
        {
            var appointments = await _appointmentsRepository.GetAvailablesByDoctor(doctorId);

            var servicePeriod = await _servicePeriodRepository.GetByDoctorId(doctorId);

            return new(appointments, doctorId, servicePeriod.Price);
        }
    }
}
