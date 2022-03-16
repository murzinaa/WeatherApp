using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IVisibilityInfoService
    {
        double GetAverageVisibility(int id);
        double GetMinVisibility(int id);
        double GetMaxVisibility(int id);
    }
}
