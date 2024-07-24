using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class AppointmentStatusTest
    {
        [Fact]
        public void CreatedStatus_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var status = AppointmentStatus.Created;

            // Assert
            Assert.Equal("Created", status.Name);
            Assert.Equal(0, status.Value);
        }

        [Fact]
        public void ScheduledStatus_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var status = AppointmentStatus.Scheduled;

            // Assert
            Assert.Equal("Scheduled", status.Name);
            Assert.Equal(1, status.Value);
        }

        [Fact]
        public void AcceptedStatus_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var status = AppointmentStatus.Accepted;

            // Assert
            Assert.Equal("Accepted", status.Name);
            Assert.Equal(2, status.Value);
        }

        [Fact]
        public void RefusedStatus_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var status = AppointmentStatus.Refused;

            // Assert
            Assert.Equal("Refused", status.Name);
            Assert.Equal(3, status.Value);
        }

        [Fact]
        public void CancelledStatus_ShouldHaveCorrectNameAndValue()
        {
            // Arrange & Act
            var status = AppointmentStatus.Cancelled;

            // Assert
            Assert.Equal("Cancelled", status.Name);
            Assert.Equal(4, status.Value);
        }

        [Fact]
        public void AppointmentStatus_ShouldBeSingletons()
        {
            // Assert
            Assert.Same(AppointmentStatus.Created, AppointmentStatus.Created);
            Assert.Same(AppointmentStatus.Scheduled, AppointmentStatus.Scheduled);
            Assert.Same(AppointmentStatus.Accepted, AppointmentStatus.Accepted);
            Assert.Same(AppointmentStatus.Refused, AppointmentStatus.Refused);
            Assert.Same(AppointmentStatus.Cancelled, AppointmentStatus.Cancelled);
        }
    }
}
