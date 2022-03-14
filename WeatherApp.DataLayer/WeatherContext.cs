using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.DataLayer
{
    public class WeatherContext: DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Temperature> Temperature { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }
    }
}
