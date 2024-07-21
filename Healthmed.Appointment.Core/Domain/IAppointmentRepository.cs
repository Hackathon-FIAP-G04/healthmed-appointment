namespace Healthmed.Appointment.Core.Domain
{
    public interface IAppointmentRepository
    {
        Task<Appointment> Get(Id id);

        Task<Appointment> Exists(Id doctorId, Period period);

        Task<IEnumerable<Appointment>> GetAvailablesByDoctor(Id doctorId);

        Task Save(Appointment appointment);

        Task SaveMany(IEnumerable<Appointment> appointments);

        Task Update(Appointment appointment);
    }
}
