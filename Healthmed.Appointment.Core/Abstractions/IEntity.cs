namespace Healthmed.Appointment.Core.Abstractions
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
