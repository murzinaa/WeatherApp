using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class VisibilityInfoModel
    {
        public double CurrentVisibility { get; set; }
        public double AverageVisibility { get; set; }
        public double MinVisibility { get; set; }
        public double MaxVisibility { get; set; }
    }
}
