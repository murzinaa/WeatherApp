using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class VisibilityInfoService : IVisibilityInfoService
    {
        private readonly WeatherContext _context;

        public VisibilityInfoService(WeatherContext context)
        {
            _context = context;
        }
        public double GetAverageVisibility(int id)
        {
            return _context.WeatherConditions
             .Where(w => w.CityId == id && w.IsArchieved == false)
             .Average(w => w.Visibility);
        }

        public double GetMaxVisibility(int id)
        {
            return _context.WeatherConditions
                .Where(w => w.CityId == id && w.IsArchieved == false)
                .Max(w => w.Visibility);
        }

        public double GetMinVisibility(int id)
        {
            return _context.WeatherConditions
                 .Where(w => w.CityId == id && w.IsArchieved == false)
                 .Min(w => w.Visibility);
        }
    }
}
