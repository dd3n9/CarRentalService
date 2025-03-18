using CarRentalService.Domain.Common.Exceptions.Vehicle;
using System.Text.RegularExpressions;

namespace CarRentalService.Domain.VehicleAggregate.ValueObjects
{
    public record LicensePlate
    {
        private static readonly Regex LicensePlatePattern =
            new(@"^[A-Z]{2}\d{4}[A-Z]{2}$", RegexOptions.Compiled);

        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !LicensePlatePattern.IsMatch(value))
            {
                throw new InvalidLicensePlateException();
            }

            Value = value;
        }

        public static implicit operator LicensePlate(string value)
            => new(value);

        public static implicit operator string(LicensePlate value)
            => value.Value;
    }
}
