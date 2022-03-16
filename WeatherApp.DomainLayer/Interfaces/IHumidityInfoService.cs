using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IHumidityInfoService
    {
        double GetAverageHumidity(int id);
        double GetMinHumidity(int id);
        double GetMaxHumidity(int id);
    }
}
