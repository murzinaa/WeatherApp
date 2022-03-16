using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IPressureInfoService
    {
        double GetAveragePressure(int id);
        double GetMinPressure(int id);
        double GetMaxPressure(int id);
    }
}
