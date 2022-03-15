using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WeatherApp.Models
{
    public class TemperatureModel
    {
        //public int Id { get; set; }
        public int CityId { get; set; }
        [Range(-50, 50, ErrorMessage = "Temperature must be from -50 to 50")]
        public double Degrees { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsArchieved { get; set; }
        //public virtual CityModel City { get; set; }
    }
}
