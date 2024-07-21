using Healthmed.Appointment.Core.UseCases.AcceptAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.CancelAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase;
using Healthmed.Appointment.Core.UseCases.RefuseAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.RegisterServicePeriod;
using Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Healthmed.Appointment.Infrastructure.IoC
{
    [ExcludeFromCodeCoverage]
    public static class UseCaseHostapplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection builder)
        {
            builder.AddScoped<IAcceptAppointmentUseCase, AcceptAppointmentUseCase>();
            builder.AddScoped<ICancelAppointmentUseCase, CancelAppointmentUseCase>();
            builder.AddScoped<IQueryAvailableAppointmentsUseCase, QueryAvailableAppointmentsUseCase>();
            builder.AddScoped<IRefuseAppointmentUseCase, RefuseAppointmentUseCase>();
            builder.AddScoped<IRegisterAppointmentUseCase, RegisterAppointmentUseCase>();
            builder.AddScoped<IRegisterServicePeriodUseCase, RegisterServicePeriodUseCase>();
            builder.AddScoped<IScheduleAppointmentUseCase, ScheduleAppointmentUseCase>();

            return builder;
        }
    }
}
