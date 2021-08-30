namespace TestApp.Services.RequestService.Interfaces
{
    public interface ICurrentRequestService
    {
        string UserId { get; set; }
        string Locale { get; set; }
    }
}