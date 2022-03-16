using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Repositories.Interfases
{
    public interface IWeatherRepository
    {
        Task CreateWeatherCondition(WeatherCondition weatherCondition);
        Task ArchiveWeatherCondition(int id);
        Task<WeatherCondition> GetWeatherCondition(int id);
        WeatherCondition GetFirstWeatherCondition(int id);
        Task DeleteWeatherCondition(WeatherCondition model);
        City GetWeatherHistory(string CityName);
        Task UpdateWeatherCondition(WeatherConditionDto weatherCondition, WeatherCondition weather);
    }
}
