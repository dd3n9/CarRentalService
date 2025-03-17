using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;

namespace CarRentalService.Domain.RentalPointAggregate
{
    public class RentalPoint : AggregateRoot<RentalPointId>
    {
        public RentalPointName Name { get; private set; }
        public RentalPointAddress Address { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;


        private RentalPoint() { } 

        private RentalPoint(
            RentalPointId rentalPointId,
            string name, 
            string address ) : base(rentalPointId)
        {
            Name = name;
            Address = address;
        }

        public static RentalPoint Create(
            string name,
            string address)
        {
            var rentalPoint = new RentalPoint(
                RentalPointId.CreateUnique(),
                name,
                address);

            return rentalPoint;
        }
    }
}
