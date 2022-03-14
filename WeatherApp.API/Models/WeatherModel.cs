using System.Collections.Generic;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.API.Models
{
    public class WeatherModel
    {
        //public string City { get; set; }
        public List<Temperature> Temperature{ get; set; }
    }
}
