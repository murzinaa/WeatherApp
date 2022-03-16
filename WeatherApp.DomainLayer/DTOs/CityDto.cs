using System.Collections.Generic;

namespace WeatherApp.DomainLayer.DTOs
{
    public class CityDto
    {
        //public CityDto()
        //{
        //    WeatherConditions = new HashSet<WeatherConditionDto>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<WeatherConditionDto> WeatherConditions { get; set; }
    }
}
