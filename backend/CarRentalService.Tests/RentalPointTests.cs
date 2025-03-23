using CarRentalService.Domain.RentalPointAggregate;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;
using FluentAssertions;
namespace CarRentalService.Tests
{
    public class RentalPointTests
    {
        [Fact]
        public void Create_ShouldInitializeRentalPoint_WithCorrectProperties()
        {
            // Arrange
            var name = new RentalPointName("Central Station");
            var city = "Kyiv";
            var street = "Main Street 1";

            // Act
            var rentalPoint = RentalPoint.Create(name, city, street);

            // Assert
            rentalPoint.Should().NotBeNull();
            rentalPoint.Id.Should().NotBeNull(); 
            rentalPoint.Name.Should().Be(name);
            rentalPoint.Address.Should().NotBeNull();
            rentalPoint.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Create_ShouldGenerateUniqueRentalPointId()
        {
            // Arrange
            var name = new RentalPointName("Central Station");
            var city = "Kyiv";
            var street = "Main Street 1";

            // Act
            var rentalPoint1 = RentalPoint.Create(name, city, street);
            var rentalPoint2 = RentalPoint.Create(name, city, street);

            // Assert
            rentalPoint1.Id.Should().NotBe(rentalPoint2.Id); 
        }
    }
}
