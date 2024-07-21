using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.UseCases.RefuseAppointmentUseCase
{
    public interface IRefuseAppointmentUseCase
    {
        Task RefuseAppointment(Guid appointmentId);
    }

    public class RefuseAppointmentUseCase : IRefuseAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public RefuseAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task RefuseAppointment(Guid appointmentId)
        {
            var appointment = await _repository.Get(appointmentId);

            AppointmentNotFoundException.ThrowIfNull(appointment);

            appointment.Refuse();

            await _repository.Update(appointment);
        }
    }
}
