using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class CityService : ICityService
    {
        private readonly WeatherContext _context;

        public CityService(WeatherContext context)
        {
            _context = context;
        }

        public async Task CreateCity(City city)
        {
            if (GetCityByCityName(city.Name) == null)
            {
                await _context.AddAsync(city);
                await _context.SaveChangesAsync();
            }
            // else return info that city is already in db
        }

        public async Task DeleteCity(int id)
        {
            var model = await _context.Set<City>().FindAsync(id);
            if (model != null)
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<City> GetCityByCityId(int id)
        {
            var model = await _context.Set<City>().FindAsync(id);
            return model;
        }

        public City GetCityByCityName(string cityName)
        {
            var model = _context.Cities.Where(c => c.Name == cityName).FirstOrDefault();
            return model;
        }

        //public List<Temperature> GetWeatherHistory(string CityName)
        //{
        //    var id = _context.Cities.Where(c => c.Name == CityName).FirstOrDefault().Id;
        //    var model = _context.Temperature.Where(t => t.CityId == id).ToList();
        //    //var model = _context.Cities.Where(c => c.Name == CityName).ToList();
        //    return model;
        //}
    }
}
