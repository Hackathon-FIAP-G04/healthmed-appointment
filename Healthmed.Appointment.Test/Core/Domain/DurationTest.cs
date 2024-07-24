using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class DurationTest
    {
        [Fact]
        public void Constructor_WithValidMinutes_ShouldSetMinutesProperty()
        {
            // Arrange
            int validMinutes = 10;

            // Act
            var duration = new Duration(validMinutes);

            // Assert
            Assert.Equal(validMinutes, duration.Minutes);
        }

        [Fact]
        public void Constructor_WithZeroMinutes_ShouldThrowInvalidDurationException()
        {
            // Arrange
            int invalidMinutes = 0;

            // Act & Assert
            Assert.Throws<InvalidDurationException>(() => new Duration(invalidMinutes));
        }

        [Fact]
        public void Constructor_WithNegativeMinutes_ShouldThrowInvalidDurationException()
        {
            // Arrange
            int invalidMinutes = -5;

            // Act & Assert
            Assert.Throws<InvalidDurationException>(() => new Duration(invalidMinutes));
        }

        [Fact]
        public void ToString_ShouldReturnMinutesAsString()
        {
            // Arrange
            int minutes = 10;
            var duration = new Duration(minutes);

            // Act
            var result = duration.ToString();

            // Assert
            Assert.Equal(minutes.ToString(), result);
        }

        [Fact]
        public void ImplicitConversion_FromDurationToInt_ShouldReturnMinutes()
        {
            // Arrange
            int minutes = 10;
            var duration = new Duration(minutes);

            // Act
            int result = duration;

            // Assert
            Assert.Equal(minutes, result);
        }

        [Fact]
        public void ImplicitConversion_FromIntToDuration_ShouldReturnDurationWithMinutes()
        {
            // Arrange
            int minutes = 10;

            // Act
            Duration duration = minutes;

            // Assert
            Assert.Equal(minutes, duration.Minutes);
        }
    }
}
