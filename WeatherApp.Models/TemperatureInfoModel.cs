using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class TemperatureInfoModel
    {
        public double CurrentTemperature { get; set; }
        public double AverageTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
    }
}
