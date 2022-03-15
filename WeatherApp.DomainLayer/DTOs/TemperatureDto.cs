using System;

namespace WeatherApp.DomainLayer.DTOs
{
    public class TemperatureDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public double Degrees { get; set; }
        public DateTime? DateTime { get; set; }
        public bool IsArchieved { get; set; }
        public virtual CityDto City { get; set; }
    }
}
