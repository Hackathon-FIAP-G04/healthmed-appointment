using Healthmed.Appointment.Core.Domain;
using static Healthmed.Appointment.Core.Exceptions;
using System.Globalization;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class PriceTest
    {
        [Fact]
        public void Constructor_WithValidAmount_ShouldSetAmountProperty()
        {
            // Arrange
            decimal validAmount = 100.50m;

            // Act
            var price = new Price(validAmount);

            // Assert
            Assert.Equal(validAmount, price.Amount);
        }

        [Fact]
        public void Constructor_WithNegativeAmount_ShouldThrowInvalidPriceException()
        {
            // Arrange
            decimal invalidAmount = -10.0m;

            // Act & Assert
            Assert.Throws<InvalidPriceException>(() => new Price(invalidAmount));
        }

        [Fact]
        public void ToString_ShouldReturnAmountAsString()
        {
            // Arrange
            decimal amount = 100.50m;
            var price = new Price(amount);

            // Act
            var result = price.ToString();

            // Assert
            Assert.Equal(amount.ToString(CultureInfo.InvariantCulture), result);
        }

        [Fact]
        public void ImplicitConversion_FromPriceToDecimal_ShouldReturnAmount()
        {
            // Arrange
            decimal amount = 100.50m;
            var price = new Price(amount);

            // Act
            decimal result = price;

            // Assert
            Assert.Equal(amount, result);
        }

        [Fact]
        public void ImplicitConversion_FromDecimalToPrice_ShouldReturnPrice()
        {
            // Arrange
            decimal amount = 100.50m;

            // Act
            Price price = amount;

            // Assert
            Assert.Equal(amount, price.Amount);
        }
    }
}
