namespace Healthmed.Appointment.Core.Domain
{
    public interface IServicePeriodRepository
    {
        Task Save(ServicePeriod servicePeriod);

        Task Update(ServicePeriod servicePeriod);

        Task<IEnumerable<ServicePeriod>> GetAll();

        Task<ServicePeriod> GetByDoctorId(Id doctorId);
    }
}
