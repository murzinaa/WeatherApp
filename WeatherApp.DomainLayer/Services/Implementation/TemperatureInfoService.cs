using System.Linq;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Services.Interfaces;

namespace WeatherApp.DomainLayer.Services.Implementation
{
    public class TemperatureInfoService : ITemperatureInfoService
    {
        private readonly WeatherContext _context;

        public TemperatureInfoService(WeatherContext context)
        {
            _context = context;
        }

        public double GetAverageTemperature(int id)
        {

            return _context.WeatherConditions
            .Where(w => w.CityId == id && w.IsArchieved == false)
            .Average(w => w.Degrees);
        }

        public double GetMaxTemperature(int id)
        {
            return _context.WeatherConditions
                .Where(w => w.CityId == id && w.IsArchieved == false)
                .Max(w => w.Degrees);
        }

        public double GetMinTemperature(int id)
        {
            return _context.WeatherConditions
                .Where(w => w.CityId == id && w.IsArchieved == false)
                .Min(w => w.Degrees);

        }
    }
}

