using CarRentalService.Domain.Common.Exceptions.User;
using System.Security.Cryptography;

namespace CarRentalService.Domain.UserAggregate.ValueObjects
{
    public record Token
    {
        public string Value { get; }

        public Token(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyTokenException();
            }

            Value = value;
        }

        public static Token CreateUnique()
            => new(Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)));

        public static implicit operator string(Token token)
            => token.Value;

        public static implicit operator Token(string token)
            => new(token);
    }
}
