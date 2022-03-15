using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WeatherApp.Models
{
    public class CityModel
    {
        public CityModel()
        {
            Temperature = new HashSet<TemperatureModel>();
        }

        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Maximum length of the city is 30")]
        public string Name { get; set; }
        public virtual ICollection<TemperatureModel> Temperature { get; set; }
    }
}
