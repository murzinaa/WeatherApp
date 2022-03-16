using System;

namespace WeatherApp.DataLayer.Entities
{
    public class Temperature
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public double Degrees { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsArchieved { get; set; } = false;
        public virtual City City { get; set; }
    }
}
