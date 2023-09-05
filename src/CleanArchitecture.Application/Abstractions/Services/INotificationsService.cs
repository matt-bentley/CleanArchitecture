
namespace CleanArchitecture.Core.Abstractions.Services
{
    public interface INotificationsService
    {
        Task WeatherAlertAsync(string summary, int temperatureC, DateTime date);
    }
}
