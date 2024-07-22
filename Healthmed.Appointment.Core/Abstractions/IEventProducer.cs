namespace Healthmed.Appointment.Core.Abstractions
{
    public interface IEventProducer
    {
        Task Send(IEnumerable<IDomainEvent> domainEvents);
    }
}
