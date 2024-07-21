using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases
{
    public interface IGenerateMonthlyAppointmentsUseCase
    {
        Task GenerateAppointments();
    }

    public class GenerateMonthlyAppointmentsUseCase : IGenerateMonthlyAppointmentsUseCase
    {
        private readonly IServicePeriodRepository _periodRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public GenerateMonthlyAppointmentsUseCase(IServicePeriodRepository periodRepository, IAppointmentRepository appointmentRepository)
        {
            _periodRepository = periodRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task GenerateAppointments()
        {
            var servicePeriods = await _periodRepository.GetAll();

            var appointmentsToRegister = new List<Domain.Appointment>();

            foreach (var servicePeriod in servicePeriods) 
            {
                var appointmentPeriod = new Period(
                    servicePeriod.Period.StartHour, servicePeriod.Period.StartMinute,
                    servicePeriod.Period.EndHour, servicePeriod.Period.EndMinute);

                do
                {
                    var appointmentToAdd = new Domain.Appointment(appointmentPeriod, servicePeriod.DoctorId);

                    appointmentsToRegister.Add(appointmentToAdd);

                    appointmentPeriod = appointmentPeriod.NextPeriod(servicePeriod.Duration);

                } while (appointmentPeriod.EndHour > servicePeriod.Period.EndHour ||
                        (appointmentPeriod.EndHour == servicePeriod.Period.EndHour &&
                         appointmentPeriod.EndMinute > servicePeriod.Period.EndMinute));
            }

            await _appointmentRepository.SaveMany(appointmentsToRegister);
        }
    }
}
