using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.UseCases.AcceptAppointmentUseCase
{
    public interface IAcceptAppointmentUseCase
    {
        Task AcceptAppointment(Guid appointmentId);
    }

    public class AcceptAppointmentUseCase : IAcceptAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public AcceptAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task AcceptAppointment(Guid appointmentId)
        {
            var appointment = await _repository.Get(appointmentId);

            AppointmentNotFoundException.ThrowIfNull(appointment);

            appointment.Accept();

            await _repository.Update(appointment);
        }
    }
}
