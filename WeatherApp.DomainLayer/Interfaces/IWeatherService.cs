using System.Threading.Tasks;
using WeatherApp.APIProviders.Models;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetCurrentWeatherByCity(string url);
    }
}
