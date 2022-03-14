using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherContext _context;

        public WeatherService(WeatherContext context)
        {
            _context = context;
        }
        public async Task CreateWeatherCondition(Temperature temperature)
        {
            await _context.AddAsync(temperature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWeatherCondition(int id)
        {
            var model = await _context.Set<Temperature>().FindAsync(id);
            if (model != null)
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public List<City> GetCurrentWeather(string CityName)
        {
            throw new NotImplementedException();
        }

        //public List<City> GetCurrentWeather(string CityName)
        //{
        //   var model =  _context.Cities.Where(c => c.Name == CityName).ToList();
        //    return model;
        //}

        public List<Temperature> GetWeatherHistory(string CityName)
        {
            var id = _context.Cities.Where(c => c.Name == CityName).FirstOrDefault().Id;
            var model = _context.Temperature.Where(t => t.CityId == id).ToList();
            //var model = _context.Cities.Where(c => c.Name == CityName).ToList();
            return model;
        }

        public async Task UpdateWeatherCondition(Temperature temperature)
        {
           var temp = _context.Temperature.Where(t => t.Id == temperature.Id).ToList().FirstOrDefault<Temperature>();

            if (temp != null)
            {
                temp.CityId = temperature.CityId;
                temp.Degrees = temperature.Degrees;
                temp.DateTime = temperature.DateTime;

                await _context.SaveChangesAsync();

            }
            //else
            //{
            //    return NotFound();
            //}
            //_context.Update(temperature);
            //await _context.SaveChangesAsync();

        }


    }
}
