using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class PressureInfoModel
    {
        public double CurrentPressure { get; set; }
        public double AveragePressure { get; set; }
        public double MinPressure { get; set; }
        public double MaxPressure { get; set; }
    }
}
