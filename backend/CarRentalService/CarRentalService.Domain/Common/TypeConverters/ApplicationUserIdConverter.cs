using CarRentalService.Domain.UserAggregate.ValueObjects;
using System.ComponentModel;
using System.Globalization;

namespace CarRentalService.Domain.Common.TypeConverters
{
    public class ApplicationUserIdConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                return new UserId(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is UserId applicationUserId)
            {
                return applicationUserId.Value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
