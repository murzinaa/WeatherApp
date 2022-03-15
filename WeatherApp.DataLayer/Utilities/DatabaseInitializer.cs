using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.DataLayer.Utilities
{
    public static class DatabaseInitializer
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<City>().HasData(
                new City() { Id = 1, Name = "Kyiv" },
                new City() { Id = 2, Name = "Lviv" },
                new City() { Id = 3, Name = "Kharkiv" });
            builder.Entity<Temperature>().HasData(
                new Temperature() { Id = 1, CityId = 1, Degrees = 12, DateTime = DateTime.Now},
                new Temperature() { Id = 2, CityId = 3, Degrees = 0, DateTime = new DateTime(2022, 3, 14, 12, 2, 30)},
                new Temperature() { Id = 3, CityId = 2, Degrees = -5, DateTime = new DateTime(2022, 3, 14, 13, 30, 30) },
                new Temperature() { Id = 4, CityId = 2, Degrees = 10, DateTime = new DateTime(2022, 3, 15, 9, 20, 59) }
                );
        }
    }
}