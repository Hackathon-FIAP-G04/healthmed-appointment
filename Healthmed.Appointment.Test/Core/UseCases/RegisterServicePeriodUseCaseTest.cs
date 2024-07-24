using Healthmed.Appointment.Core.Domain;
using Healthmed.Appointment.Core.UseCases.RegisterServicePeriod;
using Moq;

namespace Healthmed.Appointment.Test.Core.UseCases
{
    public class RegisterServicePeriodUseCaseTest
    {
        [Fact]
        public async Task RegisterServicePeriod_WithExistingServicePeriod_ShouldUpdateServicePeriod()
        {
            // Arrange
            var mockRepository = new Mock<IServicePeriodRepository>();
            var doctorId = Guid.NewGuid();
            var period = new Period(9, 30, 18, 30);
            var duration = new Duration(60);
            var price = new Price(100.0m);

            var request = new RegisterServicePeriodRequest
            {
                DoctorId = doctorId,
                StartTime = new RegisterServicePeriodTimeRequest(period.StartHour, period.StartMinute),
                EndTime = new(period.EndHour, period.EndMinute),
                Duration = duration,
                Price = price
            };

            var existingServicePeriod = new ServicePeriod(doctorId, period, duration, price);
            mockRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync(existingServicePeriod);

            var useCase = new RegisterServicePeriodUseCase(mockRepository.Object);

            // Act
            var result = await useCase.RegisterServicePeriod(request);

            // Assert
            existingServicePeriod.Update(period, duration, price);
            mockRepository.Verify(repo => repo.Update(existingServicePeriod), Times.Once);
            mockRepository.Verify(repo => repo.Save(It.IsAny<ServicePeriod>()), Times.Never);
            Assert.Equal(existingServicePeriod.Period.StartHour, result.StartTime.Hour);
            Assert.Equal(existingServicePeriod.Period.StartMinute, result.StartTime.Minute);
            Assert.Equal(existingServicePeriod.Period.EndHour, result.EndTime.Hour);
            Assert.Equal(existingServicePeriod.Period.EndMinute, result.EndTime.Minute);
        }

        [Fact]
        public async Task RegisterServicePeriod_WithNewServicePeriod_ShouldSaveServicePeriod()
        {
            // Arrange
            var mockRepository = new Mock<IServicePeriodRepository>();
            var doctorId = Guid.NewGuid();
            var duration = new Duration(60);
            var price = new Price(100.0m);
            var period = new Period(9, 0, 17, 0);

            var request = new RegisterServicePeriodRequest
            {
                DoctorId = doctorId,
                StartTime = new(period.StartHour, period.StartMinute),
                EndTime = new(period.EndHour, period.EndMinute),
                Duration = duration,
                Price = price
            };

            mockRepository.Setup(repo => repo.GetByDoctorId(doctorId)).ReturnsAsync((ServicePeriod)null);

            var useCase = new RegisterServicePeriodUseCase(mockRepository.Object);

            // Act
            var result = await useCase.RegisterServicePeriod(request);

            // Assert
            var expectedServicePeriod = new ServicePeriod(doctorId, period, duration, price);
            mockRepository.Verify(repo => repo.Save(It.Is<ServicePeriod>(sp => sp.DoctorId.Value == doctorId)), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<ServicePeriod>()), Times.Never);
            Assert.Equal(expectedServicePeriod.Period.StartHour, result.StartTime.Hour);
            Assert.Equal(expectedServicePeriod.Period.StartMinute, result.StartTime.Minute);
            Assert.Equal(expectedServicePeriod.Period.EndHour, result.EndTime.Hour);
            Assert.Equal(expectedServicePeriod.Period.EndMinute, result.EndTime.Minute);
        }
    }
}
