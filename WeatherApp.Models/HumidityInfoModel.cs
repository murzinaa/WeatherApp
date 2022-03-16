using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class HumidityInfoModel
    {
        public double CurrentHumidity { get; set; }
        public double AverageHumidity { get; set; }
        public double MinHumidity { get; set; }
        public double MaxHumidity { get; set; }
    }
}
