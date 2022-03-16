using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class PressureInfoService : IPressureInfoService
    {
        private readonly WeatherContext _context;

        public PressureInfoService(WeatherContext context)
        {
            _context = context;
        }

        public double GetAveragePressure(int id)
        {
            return _context.WeatherConditions
            .Where(w => w.CityId == id && w.IsArchieved == false)
            .Average(w => w.Pressure);
        }

        public double GetMaxPressure(int id)
        {
            return _context.WeatherConditions
               .Where(w => w.CityId == id && w.IsArchieved == false)
               .Max(w => w.Pressure);
        }

        public double GetMinPressure(int id)
        {
            return _context.WeatherConditions
                .Where(w => w.CityId == id && w.IsArchieved == false)
                .Min(w => w.Pressure);
        }
    }
}
