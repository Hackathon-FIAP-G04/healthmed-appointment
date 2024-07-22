using Healthmed.Appointment.Core.Abstractions;
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
        private readonly IEventProducer _eventProducer;

        public AcceptAppointmentUseCase(IAppointmentRepository repository, IEventProducer eventProducer)
        {
            _repository = repository;
            _eventProducer = eventProducer;
        }

        public async Task AcceptAppointment(Guid appointmentId)
        {
            var appointment = await _repository.Get(appointmentId);

            AppointmentNotFoundException.ThrowIfNull(appointment);

            appointment.Accept();

            await _repository.Update(appointment);

            if(appointment.Events.Any())
                await _eventProducer.Send(appointment.Events);
        }
    }
}
