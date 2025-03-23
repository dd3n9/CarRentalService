namespace CarRentalService.Application.Common.Clients
{
    public interface IReservationClient
    {
        Task ReceiveNotification(string message);
    }
}
