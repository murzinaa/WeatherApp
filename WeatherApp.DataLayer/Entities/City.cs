using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.DataLayer.Entities
{
    public class City
    {
        public City()
        {
            Temperature = new HashSet<Temperature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Temperature> Temperature { get; set; }
    }
}
