using System;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        public double Degrees { get; set; }
        public double Humidity { get; set; }
        public double Visibility { get; set; }
        public double Pressure { get; set; }
        public DateTime DateTime { get; set; }
    }
}
