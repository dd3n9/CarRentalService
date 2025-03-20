namespace CarRentalService.Contracts.RentalPoints.Requests
{
    public record CreateRentalPointRequest(
        string Name,
        string City, 
        string Street
        );
}
