namespace CarRentalService.Contracts.Common.DTOs
{
    public record AuthenticationDto(string UserId, string FirstName, string LastName, IEnumerable<string> UserRoles);
}
