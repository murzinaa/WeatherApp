using System.Threading.Tasks;
using WeatherApp.APIProviders.Models;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Services.Interfaces
{
    public interface IWeatherService
    {
        Task CreateWeatherCondition(WeatherConditionDto weatherCondition);
        Task UpdateWeatherCondition(WeatherConditionDto weatherCondition);
        Task DeleteWeatherCondition(int id);
        City GetWeatherHistory(string CityName);
        Task<WeatherResult> GetCurrentWeather(string url, int id);
        Task ArchiveWeatherCondition(int id);


    }
}
