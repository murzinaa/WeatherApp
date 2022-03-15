using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class StatisticalInfoService : IStatisticalInfoService
    {
        private readonly WeatherContext _context;

        public StatisticalInfoService(WeatherContext context)
        {
            _context = context;
        }

        public double GetAverage(int id)
        {
            //try
            //{
            
           // if ( await _cityService.GetCityByCityId(id) != null) 
           // {
                double avg = _context.Temperature
                .Where(t => t.CityId == id && t.IsArchieved == false)
                .Average(t => t.Degrees);
                return avg;
           // }
           // throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            //}
            //catch
            //{

            //    throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            //}

        }

        public double GetMax(int id)
        {
            //if (await _cityService.GetCityByCityId(id) != null)
           // {
                double max = _context.Temperature
                .Where(t => t.CityId == id && t.IsArchieved == false)
                .Max(t => t.Degrees);
                return max;
           // }
          //  throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            
        }

        public double GetMin(int id)
        {
           // if (await _cityService.GetCityByCityId(id) != null)
           // {
                double min = _context.Temperature
                .Where(t => t.CityId == id && t.IsArchieved == false)
                .Min(t => t.Degrees);
                return min;
            //}
           // throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            
        }
    }
}
