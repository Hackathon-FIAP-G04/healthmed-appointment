using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Infrastructure.Repositories
{
    public class ServicePeriodRepository : IServicePeriodRepository
    {
        public Task<IEnumerable<ServicePeriod>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServicePeriod> GetByDoctorId(Id doctorId)
        {
            throw new NotImplementedException();
        }

        public Task Save(ServicePeriod servicePeriod)
        {
            throw new NotImplementedException();
        }

        public Task Update(ServicePeriod servicePeriod)
        {
            throw new NotImplementedException();
        }
    }
}
