﻿using System;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        //public string City { get; set; }
        //public List<Temperature> Temperature{ get; set; }
        //public string CityName { get; set; }    
        //public int CityId { get; set; }
        //public List<Temperature> Temperature { get; set; }
        public double Degrees { get; set; }
        public double Humidity { get; set; }
        public double Visibility { get; set; }
        public double Pressure { get; set; }
        public DateTime DateTime { get; set; }
    }
}
