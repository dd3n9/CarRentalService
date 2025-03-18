namespace CarRentalService.Infrastructure.EF.Models
{
    internal class BaseReadModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
