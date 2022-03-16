using System;

namespace WeatherApp.DataLayer.Entities
{
    public class WeatherCondition
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public double Degrees { get; set; }
        public double Humidity { get; set; }
        public double Visibility { get; set; }
        public double Pressure { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsArchieved { get; set; } = false;
        public virtual City City { get; set; }
    }
}
