using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.DataLayer.Entities
{
    public class Temperature
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public double Degrees { get; set; }
        public DateTime DateTime { get; set; }
        public virtual City City { get; set; }
    }
}
