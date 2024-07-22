using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Healthmed.Appointment.Infrastructure.IoC;
using Healthmed.Appointment.Infrastructure.MongoDb.Extensions;
using Microsoft.AspNetCore.Builder;
using MassTransit;
using Healthmed.Appointment.Infrastructure.SQS;

namespace Healthmed.Appointment.Infrastructure.WebAPI
{
    [ExcludeFromCodeCoverage]
    public static class WebApiHostApplicationExtensions
    {
        public static IHostApplicationBuilder AddWebApi(this IHostApplicationBuilder builder)
        {
            builder.Services.AddProblemDetails();

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddSQSQueue(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRepositories();
            builder.Services.AddUseCases();
            builder.Services.AddMongoDb(builder.Configuration);
            builder.Services.AddHealthChecks();

            return builder;

        }

        public static WebApplication UseWebApi(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseMiddleware(typeof(CustomExceptionHandler));
            app.UseHttpsRedirection();
            app.MapControllers();
            app.MapHealthChecks("/hc");

            return app;
        }
    }
}