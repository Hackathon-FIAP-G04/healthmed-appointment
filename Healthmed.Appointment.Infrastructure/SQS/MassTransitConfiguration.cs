using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.SQS
{
    [ExcludeFromCodeCoverage]
    public class MassTransitConfiguration
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string AccessSecret { get; set; }
        public string SQSAddress { get; set; }
    }
}
