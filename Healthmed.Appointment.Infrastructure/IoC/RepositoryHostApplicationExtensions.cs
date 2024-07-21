using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryHostApplicationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection builder)
        {
            builder.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.AddScoped<IServicePeriodRepository, ServicePeriodRepository>();
            return builder;
        }
    }
}
