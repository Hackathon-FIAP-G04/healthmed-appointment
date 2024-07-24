using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class PeriodTest
    {
        [Fact]
        public void Constructor_WithValidTimes_ShouldSetProperties()
        {
            // Arrange
            int startHour = 9;
            int startMinute = 30;
            int endHour = 10;
            int endMinute = 45;

            // Act
            var period = new Period(startHour, startMinute, endHour, endMinute);

            // Assert
            Assert.Equal(startHour, period.StartHour);
            Assert.Equal(startMinute, period.StartMinute);
            Assert.Equal(endHour, period.EndHour);
            Assert.Equal(endMinute, period.EndMinute);
        }

        [Theory]
        [InlineData(-1, 0, 10, 0)]
        [InlineData(24, 0, 10, 0)]
        [InlineData(10, -1, 10, 0)]
        [InlineData(10, 60, 10, 0)]
        [InlineData(10, 0, -1, 0)]
        [InlineData(10, 0, 24, 0)]
        [InlineData(10, 0, 10, -1)]
        [InlineData(10, 0, 10, 60)]
        public void Constructor_WithInvalidHourOrMinute_ShouldThrowInvalidHourMinuteException(
            int startHour, int startMinute, int endHour, int endMinute)
        {
            // Act & Assert
            Assert.Throws<InvalidHourMinuteException>(() => new Period(startHour, startMinute, endHour, endMinute));
        }

        [Theory]
        [InlineData(10, 30, 9, 30)]
        [InlineData(10, 30, 10, 30)]
        [InlineData(10, 30, 10, 29)]
        public void Constructor_WithInvalidPeriod_ShouldThrowInvalidPeriodException(
            int startHour, int startMinute, int endHour, int endMinute)
        {
            // Act & Assert
            Assert.Throws<InvalidPeriodException>(() => new Period(startHour, startMinute, endHour, endMinute));
        }

        [Fact]
        public void NextPeriod_WithValidMinutes_ShouldReturnCorrectPeriod()
        {
            // Arrange
            var period = new Period(9, 30, 10, 30);
            int minutesToAdd = 30;

            // Act
            var nextPeriod = period.NextPeriod(minutesToAdd);

            // Assert
            Assert.Equal(10, nextPeriod.StartHour);
            Assert.Equal(0, nextPeriod.StartMinute);
            Assert.Equal(11, nextPeriod.EndHour);
            Assert.Equal(0, nextPeriod.EndMinute);
        }

        [Fact]
        public void NextPeriod_WithMinutesCausingHourOverflow_ShouldReturnCorrectPeriod()
        {
            // Arrange
            var period = new Period(9, 45, 10, 45);
            int minutesToAdd = 30;

            // Act
            var nextPeriod = period.NextPeriod(minutesToAdd);

            // Assert
            Assert.Equal(10, nextPeriod.StartHour);
            Assert.Equal(15, nextPeriod.StartMinute);
            Assert.Equal(11, nextPeriod.EndHour);
            Assert.Equal(15, nextPeriod.EndMinute);
        }

        [Fact]
        public void NextPeriod_WithMinutesCausingMultipleHourOverflow_ShouldReturnCorrectPeriod()
        {
            // Arrange
            var period = new Period(22, 30, 23, 30);
            int minutesToAdd = 120;

            // Act
            var nextPeriod = period.NextPeriod(minutesToAdd);

            // Assert
            Assert.Equal(0, nextPeriod.StartHour);
            Assert.Equal(30, nextPeriod.StartMinute);
            Assert.Equal(1, nextPeriod.EndHour);
            Assert.Equal(30, nextPeriod.EndMinute);
        }
    }
}
