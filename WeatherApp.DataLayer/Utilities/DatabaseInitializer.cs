using Microsoft.EntityFrameworkCore;
using System;
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
            builder.Entity<WeatherCondition>().HasData(
                new WeatherCondition() { Id = 1, CityId = 1, Degrees = 12, Humidity = 80, Pressure = 10, Visibility = 100, DateTime = DateTime.Now},
                new WeatherCondition() { Id = 2, CityId = 3, Degrees = 0, Humidity = 2, Pressure = 100, Visibility = 0, DateTime = new DateTime(2022, 3, 14, 12, 2, 30)},
                new WeatherCondition() { Id = 3, CityId = 2, Degrees = -5, Humidity = 33, Pressure = 60, Visibility = 50, DateTime = new DateTime(2022, 3, 14, 13, 30, 30) },
                new WeatherCondition() { Id = 4, CityId = 2, Degrees = 10, Humidity = 100, Pressure = 100, Visibility = 100, DateTime = new DateTime(2022, 3, 15, 9, 20, 59) }
                );
        }
    }
}