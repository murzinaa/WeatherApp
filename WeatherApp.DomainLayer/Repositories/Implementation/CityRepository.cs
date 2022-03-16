using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Repositories.Interfases;

namespace WeatherApp.DomainLayer.Repositories.Implementation
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherContext _context;

        public CityRepository(WeatherContext context)
        {
            _context = context;
        }

        public async Task CreateCity(City city)
        {
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCity(City city)
        {
            _context.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<City> GetCityByCityId(int id)
        {
            return await _context.Set<City>().FindAsync(id);
        }

        public async Task<City> GetCityByCityName(string cityName)
        {
            return  await _context.Cities.Where(c => c.Name == cityName).FirstOrDefaultAsync(); ;
        }
    }
}
