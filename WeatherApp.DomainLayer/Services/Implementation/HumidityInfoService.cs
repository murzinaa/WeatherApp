using System.Linq;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Services.Interfaces;

namespace WeatherApp.DomainLayer.Services.Implementation
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
