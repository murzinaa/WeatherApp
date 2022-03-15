﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.DataLayer.Entities
{
    public class City
    {
        public City()
        {
            Temperature = new HashSet<Temperature>();
        }

        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Maximum length of this field is 30")]
        public string Name { get; set; }
        public virtual ICollection<Temperature> Temperature { get; set; }
    }
}
