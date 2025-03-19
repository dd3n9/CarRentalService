using CarRentalService.Domain.Common.Exceptions.User;

namespace CarRentalService.Domain.UserAggregate.ValueObjects
{
    public record RefreshTokenId
    {
        public Guid Value { get; }

        public RefreshTokenId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyRefreshTokenIdException();

            Value = value;
        }

        public static RefreshTokenId CreateUnique()
        {
            return new RefreshTokenId(Guid.NewGuid());
        }

        public static implicit operator RefreshTokenId(Guid refreshTokenId)
            => new RefreshTokenId(refreshTokenId);

        public static implicit operator Guid(RefreshTokenId refreshTokenId)
            => refreshTokenId.Value;
    }
}
