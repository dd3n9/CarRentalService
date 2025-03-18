using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.UserAggregate.Entities;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CarRentalService.Domain.UserAggregate
{
    public class User : IdentityUser<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;


        private readonly List<IDomainEvent> _domainEvents = new();
        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private User(
            UserId userId,
            string firstName,
            string lastName,
            string email, 
            string password)
        {
            Id = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = password;
        }

        private User() { }

        public static User Create(
            string firstName,
            string lastName,
            string email, 
            string password)
        {
            var user = new User(
                UserId.CreateUnique(),
                firstName,
                lastName,
                email, 
                password
                );

            return user;
        }

        public RefreshToken AddRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
            return refreshToken;
        }

        public void RemoveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Remove(refreshToken);
        }

        public void RevokeAllRefreshTokens()
        {
            _refreshTokens.Clear();
        }

        public RefreshToken? FindRefreshToken(Token token)
        {
            return _refreshTokens.FirstOrDefault(rt => rt.Token == token);
        }
    }
}
