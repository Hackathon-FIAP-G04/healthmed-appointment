using Healthmed.Appointment.Core.Abstractions;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Healthmed.Appointment.Infrastructure.SQS
{
    public class EventProducer : IEventProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly Uri _endpoint;

        public EventProducer(ISendEndpointProvider sendEndpointProvider, IOptions<MassTransitConfiguration> options)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _endpoint = new Uri(options.Value.SQSAddress);
        }

        public async Task Send(IEnumerable<IDomainEvent> domainEvents)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(_endpoint);
            await endpoint.SendBatch(domainEvents);
        }
    }
}
