using Microsoft.EntityFrameworkCore;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DataLayer.Utilities;

namespace WeatherApp.DataLayer
{
    public class WeatherContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Temperature> Temperature { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
            //Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<City>().
                HasMany(c => c.Temperature)
                .WithOne(t => t.City)
                .HasForeignKey(t => t.CityId);


            builder.Seed();

        }
    }
}