using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.RentalPointAggregate.ValueObjects;

namespace CarRentalService.Domain.RentalPointAggregate
{
    public class RentalPoint : AggregateRoot<RentalPointId>
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

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
