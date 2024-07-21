using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Core.UseCases.RegisterServicePeriod
{
    public interface IRegisterServicePeriodUseCase
    {
        Task<RegisterServicePeriodResponse> RegisterServicePeriod(RegisterServicePeriodRequest request);
    }

    public class RegisterServicePeriodUseCase : IRegisterServicePeriodUseCase
    {
        private readonly IServicePeriodRepository _repository;

        public RegisterServicePeriodUseCase(IServicePeriodRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterServicePeriodResponse> RegisterServicePeriod(RegisterServicePeriodRequest request)
        {
            var period = new Period(
                request.StartTime.Hour, request.StartTime.Minute,
                request.EndTime.Hour, request.EndTime.Minute);

            var servicePeriod = await _repository.GetByDoctorId(request.DoctorId);
            if(servicePeriod != null)
            {
                servicePeriod.Update(period, request.Duration, request.Price);
                await _repository.Update(servicePeriod);
            }
            else
            {
                servicePeriod = new ServicePeriod(request.DoctorId, period, request.Duration, request.Price);
                await _repository.Save(servicePeriod);
            }

            return new(servicePeriod);
        }
    }
}
