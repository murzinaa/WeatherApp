using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Repositories.Interfases;

namespace WeatherApp.DomainLayer.Repositories.Implementation
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherContext _context;

        public WeatherRepository(WeatherContext context)
        {
            _context = context;
        }

        public async Task ArchiveWeatherCondition(int id)
        {
            var weather = _context.WeatherConditions.Where(t => t.Id == id).ToList().First<WeatherCondition>();
            weather.IsArchieved = true;

            await _context.SaveChangesAsync();
        }

        public async Task CreateWeatherCondition(WeatherCondition weatherCondition)
        {
            await _context.AddAsync(weatherCondition);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWeatherCondition(WeatherCondition model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public WeatherCondition GetFirstWeatherCondition(int id)
        {
            return _context.WeatherConditions.Where(t => t.Id == id).ToList().FirstOrDefault();
        }

        public async Task<WeatherCondition> GetWeatherCondition(int id)
        {
            return  await _context.Set<WeatherCondition>().FindAsync(id);
        }

        public City GetWeatherHistory(string CityName)
        {
            return  _context.Cities.Include(c => c.WeatherConditions).Where(c => c.Name == CityName).FirstOrDefault();
        }

        public async Task UpdateWeatherCondition(WeatherConditionDto weatherCondition, WeatherCondition weather)
        {
            weather.CityId = weatherCondition.CityId;
            weather.Degrees = weatherCondition.Degrees;
            weather.DateTime = weatherCondition.DateTime;
            weather.Visibility = weatherCondition.Visibility;
            weather.Humidity = weatherCondition.Humidity;
            weather.Pressure = weatherCondition.Pressure;

            await _context.SaveChangesAsync();
        }
    }
}
