using Healthmed.Appointment.Core.Abstractions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.SQS
{
    [ExcludeFromCodeCoverage]
    public static class MassTransitHostApplicationExtensions
    {
        public static IServiceCollection AddSQSQueue(this IServiceCollection services, IConfiguration configuration)
        {
            var awsConfiguration = configuration
                .GetRequiredSection("MassTransit")
                .Get<MassTransitConfiguration>();

            services.Configure<MassTransitConfiguration>(x => 
                new MassTransitConfiguration()
                {
                    SQSAddress = awsConfiguration.SQSAddress
                });

            services.AddMassTransit(x =>
            {
                x.UsingAmazonSqs((context, cfg) =>
                {
                    cfg.Host(awsConfiguration.Region, h =>
                    {
                        h.AccessKey(awsConfiguration.AccessKey);
                        h.SecretKey(awsConfiguration.AccessSecret);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IEventProducer, EventProducer>();

            return services;
        }
    }
}
