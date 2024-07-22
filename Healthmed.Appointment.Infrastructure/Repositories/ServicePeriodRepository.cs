using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Healthmed.Appointment.Infrastructure.Repositories
{
    public class ServicePeriodRepository : IServicePeriodRepository
    {
        private readonly IMongoCollection<ServicePeriod> _services;

        public ServicePeriodRepository(IDbContext context)
        {
            _services = context.Database.GetCollection<ServicePeriod>("servicePeriods");
        }

        public async Task<IEnumerable<ServicePeriod>> GetAll()
        {
            return await _services.Find(x => true).ToListAsync();
        }

        public async Task<ServicePeriod> GetByDoctorId(Id doctorId)
        {
            return await _services.Find(x => x.DoctorId == doctorId).FirstOrDefaultAsync();
        }

        public async Task Save(ServicePeriod servicePeriod)
        {
            await _services.InsertOneAsync(servicePeriod);
        }

        public async Task Update(ServicePeriod servicePeriod)
        {
            var update = Builders<ServicePeriod>.Update
                .Set(a => a.Period, servicePeriod.Period)
                .Set(a => a.Duration, servicePeriod.Duration)
                .Set(a => a.Price, servicePeriod.Price);

            await _services.UpdateOneAsync(a => a.Id == servicePeriod.Id, update, null);
        }
    }
}
