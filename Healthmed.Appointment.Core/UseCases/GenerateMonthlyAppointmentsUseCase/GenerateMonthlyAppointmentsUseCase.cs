using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.GenerateMonthlyAppointmentsUseCase
{
    public interface IGenerateMonthlyAppointmentsUseCase
    {
        Task GenerateAppointments(Guid doctorId);
    }

    public class GenerateMonthlyAppointmentsUseCase : IGenerateMonthlyAppointmentsUseCase
    {
        private readonly IServicePeriodRepository _periodRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        private readonly int _daysToSchedule = 30;

        public GenerateMonthlyAppointmentsUseCase(IServicePeriodRepository periodRepository, IAppointmentRepository appointmentRepository)
        {
            _periodRepository = periodRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task GenerateAppointments(Guid doctorId)
        {
            var servicePeriod = await _periodRepository.GetByDoctorId(doctorId);

            var appointmentsToRegister = new List<Domain.Appointment>();

            for (int day = 1; day <= _daysToSchedule; day++)
            {
                var startTime = DateTime.Now.AddDays(day).Date.AddHours(servicePeriod.Period.StartHour).AddMinutes(servicePeriod.Period.StartMinute);
                var appointmentPeriod = new SchedulingPeriod(startTime, servicePeriod.Duration);

                do
                {
                    var appointmentToAdd = new Domain.Appointment(appointmentPeriod, servicePeriod.DoctorId);

                    appointmentsToRegister.Add(appointmentToAdd);

                    appointmentPeriod = appointmentPeriod.Next(servicePeriod.Duration);

                } while (appointmentPeriod.EndTime.Hour > servicePeriod.Period.EndHour ||
                        appointmentPeriod.EndTime.Hour == servicePeriod.Period.EndHour &&
                         appointmentPeriod.EndTime.Minute > servicePeriod.Period.EndMinute);
            }

            await _appointmentRepository.SaveMany(appointmentsToRegister);
        }
    }
}
