using Healthmed.Appointment.Core.Domain;

namespace Healthmed.Appointment.Test.Core.Domain
{
    public class IdTest
    {
        [Fact]
        public void Constructor_WithValidGuid_ShouldSetValue()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            Id id = guid;

            // Assert
            Assert.Equal(guid, id.Value);
        }

        [Fact]
        public void Constructor_WithValidString_ShouldSetValue()
        {
            // Arrange
            var guidString = Guid.NewGuid().ToString();

            // Act
            Id id = guidString;

            // Assert
            Assert.Equal(Guid.Parse(guidString), id.Value);
        }

        [Fact]
        public void Undefined_ShouldReturnIdWithEmptyGuid()
        {
            // Act
            var id = Id.Undefined;

            // Assert
            Assert.Equal(Guid.Empty, id.Value);
        }

        [Fact]
        public void New_ShouldReturnIdWithNewGuid()
        {
            // Act
            var id = Id.New();

            // Assert
            Assert.NotEqual(Guid.Empty, id.Value);
            Assert.NotEqual(Guid.Empty, id.Value);
        }

        [Fact]
        public void ImplicitConversion_FromGuidToId_ShouldReturnId()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            Id id = guid;

            // Assert
            Assert.Equal(guid, id.Value);
        }

        [Fact]
        public void ImplicitConversion_FromIdToGuid_ShouldReturnGuid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id = new Id(guid);

            // Act
            Guid result = id;

            // Assert
            Assert.Equal(guid, result);
        }

        [Fact]
        public void ImplicitConversion_FromStringToId_ShouldReturnId()
        {
            // Arrange
            var guidString = Guid.NewGuid().ToString();

            // Act
            Id id = guidString;

            // Assert
            Assert.Equal(Guid.Parse(guidString), id.Value);
        }

        [Fact]
        public void IsNullOrEmpty_ShouldReturnTrueForEmptyGuid()
        {
            // Arrange
            var id = Id.Undefined;

            // Act
            var result = id.IsNullOrEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ShouldReturnFalseForNonEmptyGuid()
        {
            // Arrange
            var id = Id.New();

            // Act
            var result = id.IsNullOrEmpty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameGuid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id1 = new Id(guid);
            var id2 = new Id(guid);

            // Act
            var result = id1.Equals(id2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalseForDifferentGuids()
        {
            // Arrange
            var id1 = Id.New();
            var id2 = Id.New();

            // Act
            var result = id1.Equals(id2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnFalseForNull()
        {
            // Arrange
            var id = Id.New();

            // Act
            var result = id.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameInstance()
        {
            // Arrange
            var id = Id.New();

            // Act
            var result = id.Equals(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturnTrueForSameGuidAsString()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id = new Id(guid);
            var guidString = guid.ToString();

            // Act
            var result = id.Equals(guidString);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashCodeForSameGuid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id1 = new Id(guid);
            var id2 = new Id(guid);

            // Act
            var hash1 = id1.GetHashCode();
            var hash2 = id2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentGuids()
        {
            // Arrange
            var id1 = Id.New();
            var id2 = Id.New();

            // Act
            var hash1 = id1.GetHashCode();
            var hash2 = id2.GetHashCode();

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
