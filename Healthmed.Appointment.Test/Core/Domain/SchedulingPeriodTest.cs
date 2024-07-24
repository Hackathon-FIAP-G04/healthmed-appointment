using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class SchedulingPeriodTest
    {
        [Fact]
        public void Constructor_WithValidStartAndEndTime_ShouldSetProperties()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 9, 0, 0);
            var endTime = new DateTime(2024, 7, 23, 10, 0, 0);

            // Act
            var period = new SchedulingPeriod(startTime, endTime);

            // Assert
            Assert.Equal(startTime, period.StartTime);
            Assert.Equal(endTime, period.EndTime);
        }

        [Fact]
        public void Constructor_WithEndTimeBeforeStartTime_ShouldThrowInvalidPeriodException()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 23, 9, 0, 0);

            // Act & Assert
            Assert.Throws<InvalidPeriodException>(() => new SchedulingPeriod(startTime, endTime));
        }

        [Fact]
        public void Constructor_WithValidStartTimeAndDuration_ShouldSetProperties()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 9, 0, 0);
            int minutes = 60;
            var expectedEndTime = startTime.AddMinutes(minutes);

            // Act
            var period = new SchedulingPeriod(startTime, minutes);

            // Assert
            Assert.Equal(startTime, period.StartTime);
            Assert.Equal(expectedEndTime, period.EndTime);
        }

        [Fact]
        public void Constructor_WithNegativeDuration_ShouldThrowInvalidPeriodException()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 9, 0, 0);
            int minutes = -60;

            // Act & Assert
            Assert.Throws<InvalidPeriodException>(() => new SchedulingPeriod(startTime, minutes));
        }

        [Fact]
        public void Next_WithValidMinutes_ShouldReturnCorrectSchedulingPeriod()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 9, 0, 0);
            var endTime = new DateTime(2024, 7, 23, 10, 0, 0);
            var period = new SchedulingPeriod(startTime, endTime);
            int minutesToAdd = 30;
            var expectedStartTime = startTime.AddMinutes(minutesToAdd);
            var expectedEndTime = endTime.AddMinutes(minutesToAdd);

            // Act
            var nextPeriod = period.Next(minutesToAdd);

            // Assert
            Assert.Equal(expectedStartTime, nextPeriod.StartTime);
            Assert.Equal(expectedEndTime, nextPeriod.EndTime);
        }

        [Fact]
        public void Next_WithNegativeMinutes_ShouldReturnCorrectSchedulingPeriod()
        {
            // Arrange
            var startTime = new DateTime(2024, 7, 23, 10, 0, 0);
            var endTime = new DateTime(2024, 7, 23, 11, 0, 0);
            var period = new SchedulingPeriod(startTime, endTime);
            int minutesToAdd = -30;
            var expectedStartTime = startTime.AddMinutes(minutesToAdd);
            var expectedEndTime = endTime.AddMinutes(minutesToAdd);

            // Act
            var nextPeriod = period.Next(minutesToAdd);

            // Assert
            Assert.Equal(expectedStartTime, nextPeriod.StartTime);
            Assert.Equal(expectedEndTime, nextPeriod.EndTime);
        }
    }
}
