using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Infrastructure.MongoDb;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IMongoCollection<Core.Domain.Appointment> _appointments;

        public AppointmentRepository(IDbContext context)
        {
            _appointments = context.Database.GetCollection<Core.Domain.Appointment>("appointments");
        }

        public async Task<Core.Domain.Appointment> Exists(Id doctorId, SchedulingPeriod period)
        {
            var appointments = await _appointments.Find(x => 
                x.DoctorId == doctorId &&
                (x.Period.EndTime > period.StartTime &&
                    x.Period.EndTime < period.EndTime) ||
                (x.Period.StartTime < period.EndTime &&
                 x.Period.EndTime > period.EndTime) ||
                (x.Period.StartTime == period.StartTime &&
                 x.Period.EndTime == period.EndTime))
             .FirstOrDefaultAsync();

            return appointments;
        }

        public async Task<Core.Domain.Appointment> Get(Id id)
        {
            return await _appointments.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Core.Domain.Appointment>> GetAvailablesByDoctor(Id doctorId)
        {
            return await _appointments.Find(a =>
                    a.DoctorId == doctorId &&
                    a.PatientId == null && 
                    a.Status == AppointmentStatus.Created &&
                    a.Period.StartTime >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<Core.Domain.Appointment>> GetByPatient(Id patientId)
        {
            return await _appointments
                .Find(x => x.PatientId == patientId)
                .ToListAsync();
        }

        public async Task Save(Core.Domain.Appointment appointment)
        {
            await _appointments.InsertOneAsync(appointment);
        }

        public async Task SaveMany(IEnumerable<Core.Domain.Appointment> appointments)
        {
            await _appointments.InsertManyAsync(appointments);
        }

        public async Task Update(Core.Domain.Appointment appointment)
        {
            var update = Builders<Core.Domain.Appointment>.Update
                .Set(a => a.Status, appointment.Status)
                .Set(a => a.PatientId, appointment.PatientId);

            await _appointments.UpdateOneAsync(a => a.Id == appointment.Id, update, null);
        }
    }
}
