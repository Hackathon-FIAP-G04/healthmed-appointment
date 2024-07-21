using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase
{
    public interface IRegisterAppointmentUseCase
    {
        Task<RegisterAppointmentResponse> RegisterAppointment(RegisterAppointmentRequest request);
    }

    public class RegisterAppointmentUseCase : IRegisterAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public RegisterAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterAppointmentResponse> RegisterAppointment(RegisterAppointmentRequest request)
        {
            var period = new SchedulingPeriod(request.StartTime, request.EndTime);

            var existingAppointment = await _repository.Exists(request.DoctorId, period);

            ConflictingAppointmentException.ThrowIf(existingAppointment != null);

            var appointment = new Domain.Appointment(period, request.DoctorId);

            await _repository.Save(appointment);

            return new(appointment);
        }
    }
}
