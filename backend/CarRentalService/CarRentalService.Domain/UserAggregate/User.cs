using CarRentalService.Domain.Common.Models;
using CarRentalService.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CarRentalService.Domain.UserAggregate
{
    public class User : IdentityUser<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        //public string HashedPassword { get; private set; }


        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private User(
            UserId userId,
            string firstName,
            string lastName,
            string email)
        {
            Id = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            //HashedPassword = hashedPassword;
        }

        private User() { }

        public static User Create(
            string firstName,
            string lastName,
            string email)
        {
            var user = new User(
                UserId.CreateUnique(),
                firstName,
                lastName,
                email
                //hashedPassword
                );

            return user;
        }
    }
}
