using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class WeatherInfoModel
    {
        public string CityName { get; set; }
        public int CityId { get; set; }
        public List<WeatherModel> WeatherInfo { get; set; }
    }
}
