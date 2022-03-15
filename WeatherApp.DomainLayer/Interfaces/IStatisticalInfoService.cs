using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IStatisticalInfoService
    {
        double GetAverage (int id);
        double GetMin (int id);
        double GetMax (int id);
    }
}
