namespace CarRentalService.Application.Common.Interfaces.ReadServices
{
    public interface IUserReadService
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
