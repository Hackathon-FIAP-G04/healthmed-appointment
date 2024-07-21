using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.UseCases.CancelAppointmentUseCase
{
    public interface ICancelAppointmentUseCase
    {
        Task CancelAppointment(Guid appointmentId);
    }

    public class CancelAppointmentUseCase : ICancelAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public CancelAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task CancelAppointment(Guid appointmentId)
        {
            var appointment = await _repository.Get(appointmentId);

            AppointmentNotFoundException.ThrowIfNull(appointment);

            appointment.Cancel();

            await _repository.Update(appointment);
        }
    }
}
