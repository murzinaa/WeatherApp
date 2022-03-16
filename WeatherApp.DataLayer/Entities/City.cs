using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.DataLayer.Entities
{
    public class City
    {
        public City()
        {
            WeatherConditions = new HashSet<WeatherCondition>();
        }

        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Maximum length of this field is 30")]
        public string Name { get; set; }
        public virtual ICollection<WeatherCondition> WeatherConditions { get; set; }
    }
}
