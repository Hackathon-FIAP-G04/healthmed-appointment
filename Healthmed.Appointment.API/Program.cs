using Healthmed.Appointment.Core.UseCases.AcceptAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.CancelAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.QueryAvailableAppointmentsUseCase;
using Healthmed.Appointment.Core.UseCases.RefuseAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.RegisterAppointmentUseCase;
using Healthmed.Appointment.Core.UseCases.RegisterServicePeriod;
using Healthmed.Appointment.Core.UseCases.ScheduleAppointmentUseCase;
using Healthmed.Appointment.Infrastructure.WebAPI;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddWebApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebApi();

app.MapGet("/appointments/available", async ([FromServices] IQueryAvailableAppointmentsUseCase useCase, [FromQuery] Guid doctorId) =>
{
    return Results.Ok(await useCase.QueryAppointments(doctorId));
});

app.MapPost("/servicePeriods", async ([FromServices] IRegisterServicePeriodUseCase useCase, [FromBody] RegisterServicePeriodRequest request) =>
{
    return Results.Ok(await useCase.RegisterServicePeriod(request));
});

app.MapPost("/appointments", async ([FromServices] IRegisterAppointmentUseCase useCase, [FromBody] RegisterAppointmentRequest request) =>
{
    return Results.Ok(await useCase.RegisterAppointment(request));
});

app.MapPatch("/appointments/schedule", async ([FromServices] IScheduleAppointmentUseCase useCase, ScheduleAppointmentRequest request) =>
{
    return Results.Ok(await useCase.ScheduleAppointment(request));
});

app.MapPatch("/appointments/accept", async ([FromServices] IAcceptAppointmentUseCase useCase, [FromQuery] Guid appointmentId) =>
{
    await useCase.AcceptAppointment(appointmentId);
    return Results.Ok();
});

app.MapPatch("/appointments/refuse", async ([FromServices] IRefuseAppointmentUseCase useCase, [FromQuery] Guid appointmentId) =>
{
    await useCase.RefuseAppointment(appointmentId);
    return Results.Ok();
});

app.MapPatch("/appointments/cancel", async ([FromServices] ICancelAppointmentUseCase useCase, [FromQuery] Guid appointmentId) =>
{
    await useCase.CancelAppointment(appointmentId);
    return Results.Ok();
});

app.Run();
