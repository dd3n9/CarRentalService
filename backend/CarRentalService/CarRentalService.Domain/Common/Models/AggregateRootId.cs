using CarRentalService.Domain.Common.Exceptions.Aggregate;

namespace CarRentalService.Domain.Common.Models
{
    public abstract record AggregateRootId<TId>
    {
        public TId Value { get; protected set; }

        protected AggregateRootId(TId value)
        {
            if (value is null)
            {
                throw new EmptyAggregateRootIdException();
            }
            Value = value;
        }
    }
}
