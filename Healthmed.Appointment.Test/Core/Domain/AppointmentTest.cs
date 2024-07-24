using FluentAssertions;
using Healthmed.Appointment.Core.Domain;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class AppointmentTest
    {
        [Fact]
        public void Should_create_appointment()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            
            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);

            appointment.Should().NotBeNull();
            appointment.Id.Should().NotBeNull();
            appointment.Id.Should().NotBe(Guid.Empty);
            appointment.DoctorId.Should().Be(doctorId);
            appointment.Period.Should().Be(period);
            appointment.PatientId.Should().BeNull();
            appointment.Status.Should().Be(AppointmentStatus.Created);
        }

        [Fact]
        public void Should_schedule_appointment()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);

            appointment.Schedule(patientId);

            appointment.PatientId.Should().Be(patientId);
            appointment.Status.Should().Be(AppointmentStatus.Scheduled);
        }

        [Fact]
        public void Should_not_schedule_when_appointment_has_patient()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);

            var action = () => appointment.Schedule(patientId);

            action.Should().Throw<UnavailableAppointmentException>().WithMessage("The requested appointment is not available");
        }

        [Fact]
        public void Should_not_schedule_when_appointment_isnt_created()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);
            appointment.Cancel();

            var action = () => appointment.Schedule(patientId);

            action.Should().Throw<UnavailableAppointmentException>().WithMessage("The requested appointment is not available");
        }

        [Fact]
        public void Should_accept_appointment()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);

            appointment.Accept();

            appointment.Status.Should().Be(AppointmentStatus.Accepted);
        }

        [Fact]
        public void Should_throw_error_when_appointment_not_scheduled()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            var action = () => appointment.Accept();

            action.Should().Throw<AppointmentNotAcceptableException>().WithMessage("Only scheduled appointments can be accepted");
        }

        [Fact]
        public void Should_refuse_appointment()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);

            appointment.Refuse();

            appointment.Status.Should().Be(AppointmentStatus.Refused);
        }

        [Fact]
        public void Should_throw_error_when_refuse_appointment_not_scheduled()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            var action = () => appointment.Refuse();

            action.Should().Throw<AppointmentNotRefusableException>().WithMessage("Only scheduled appointments can be refused");
        }

        [Fact]
        public void Should_cancel_appointment_when_status_scheduled()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);

            appointment.Cancel();

            appointment.Status.Should().Be(AppointmentStatus.Cancelled);
        }

        [Fact]
        public void Should_cancel_appointment_when_status_acceptedd()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();
            var patientId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            appointment.Schedule(patientId);
            appointment.Accept();

            appointment.Cancel();

            appointment.Status.Should().Be(AppointmentStatus.Cancelled);
        }

        [Fact]
        public void Should_throw_error_when_cancel_appointment_not_scheduled_or_accepted()
        {
            var period = new SchedulingPeriod(DateTime.Now, DateTime.Now.AddMinutes(50));
            var doctorId = Guid.NewGuid();

            var appointment = new Healthmed.Appointment.Core.Domain.Appointment(period, doctorId);
            var action = () => appointment.Cancel();

            action.Should().Throw<AppointmentNotCancellableException>().WithMessage("Only scheduled or accepted appointments can be cancelled");
        }
    }
}
