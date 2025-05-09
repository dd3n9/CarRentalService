﻿namespace CarRentalService.Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
        where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public TId Id { get; protected set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity() { }

        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Id.Equals((object?)other);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public void AddDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
