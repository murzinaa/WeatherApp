using System.Threading.Tasks;
using WeatherApp.APIProviders.Models;

namespace WeatherApp.APIProviders
{
    public interface IAPIWeatherProvider
    {
        Task<WeatherResult> GetCurrentWeather(string url);
    }
}
