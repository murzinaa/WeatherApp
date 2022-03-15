using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.DomainLayer.DTOs
{
    public class CityDto
    {
        public CityDto()
        {
            Temperature = new HashSet<TemperatureDto>();
        }

        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Maximum length of this field is 30")]
        public string Name { get; set; }
        public virtual ICollection<TemperatureDto> Temperature { get; set; }
    }
}
