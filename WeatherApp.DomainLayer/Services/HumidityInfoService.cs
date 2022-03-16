using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class HumidityInfoService : IHumidityInfoService
    {
        private readonly WeatherContext _context;
        public HumidityInfoService(WeatherContext context)
        {
            _context = context;
        }
        public double GetAverageHumidity(int id)
        {
            return _context.WeatherConditions
           .Where(w => w.CityId == id && w.IsArchieved == false)
           .Average(w => w.Humidity);
        }

        public double GetMaxHumidity(int id)
        {
            return _context.WeatherConditions
               .Where(w => w.CityId == id && w.IsArchieved == false)
               .Max(w => w.Humidity);
        }

        public double GetMinHumidity(int id)
        {
            return _context.WeatherConditions
                .Where(w => w.CityId == id && w.IsArchieved == false)
                .Min(w => w.Humidity);
        }
    }
}
