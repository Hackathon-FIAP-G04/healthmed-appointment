using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class ServicePeriodTest
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldSetProperties()
        {
            // Arrange
            var doctorId = new Id(Guid.NewGuid());
            var period = new Period(9, 0, 10, 0);
            var duration = new Duration(60);
            var price = new Price(100.0m);

            // Act
            var servicePeriod = new ServicePeriod(doctorId, period, duration, price);

            // Assert
            Assert.NotEqual(Id.Undefined, servicePeriod.Id);
            Assert.Equal(doctorId, servicePeriod.DoctorId);
            Assert.Equal(period, servicePeriod.Period);
            Assert.Equal(duration, servicePeriod.Duration);
            Assert.Equal(price, servicePeriod.Price);
        }

        [Fact]
        public void Update_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var doctorId = new Id(Guid.NewGuid());
            var initialPeriod = new Period(9, 0, 10, 0);
            var initialDuration = new Duration(60);
            var initialPrice = new Price(100.0m);
            var servicePeriod = new ServicePeriod(doctorId, initialPeriod, initialDuration, initialPrice);

            var newPeriod = new Period(10, 0, 11, 0);
            var newDuration = new Duration(90);
            var newPrice = new Price(150.0m);

            // Act
            servicePeriod.Update(newPeriod, newDuration, newPrice);

            // Assert
            Assert.Equal(newPeriod, servicePeriod.Period);
            Assert.Equal(newDuration, servicePeriod.Duration);
            Assert.Equal(newPrice, servicePeriod.Price);
        }
    }
}
