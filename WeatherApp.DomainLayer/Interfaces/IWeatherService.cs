using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IWeatherService
    {
        Task CreateWeatherCondition(Temperature temperature);
        Task UpdateWeatherCondition(Temperature temperature);
        Task DeleteWeatherCondition(int id);
        List<Temperature> GetWeatherHistory(string CityName);
        List<City> GetCurrentWeather(string CityName);
        Task ArchiveWeatherCondition(int id);
        

    }
}
