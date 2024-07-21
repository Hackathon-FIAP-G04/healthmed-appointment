using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase
{
    public interface IScheduleAppointmentUseCase
    {
        Task<ScheduleAppointmentResponse> ScheduleAppointment(ScheduleAppointmentRequest request);
    }

    public class ScheduleAppointmentUseCase : IScheduleAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public ScheduleAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ScheduleAppointmentResponse> ScheduleAppointment(ScheduleAppointmentRequest request)
        {
            var appointment = await _repository.Get(request.AppointmentId);

            AppointmentNotFoundException.ThrowIfNull(appointment);

            appointment.Schedule(request.PatientId);

            await _repository.Update(appointment);

            return new(appointment);
        }
    }
}
