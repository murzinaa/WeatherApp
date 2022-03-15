using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.APIProviders.Models;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IWeatherService
    {
        Task CreateWeatherCondition(TemperatureDto temperature);
        Task UpdateWeatherCondition(TemperatureDto temperature);
        Task DeleteWeatherCondition(int id);
        List<Temperature> GetWeatherHistory(string CityName);
        Task<WeatherResult> GetCurrentWeather(string url, int id);
        Task ArchiveWeatherCondition(int id);
        

    }
}
