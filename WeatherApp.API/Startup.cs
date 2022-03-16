using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherApp.DataLayer;
using WeatherApp.API.Extensions;
using WeatherApp.APIProviders;
using FluentValidation.AspNetCore;
using FluentValidation;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Validation;
using WeatherApp.API.Helpers;
using WeatherApp.DomainLayer.Repositories.Interfases;
using WeatherApp.DomainLayer.Repositories.Implementation;
using WeatherApp.DomainLayer.Services.Implementation;
using WeatherApp.DomainLayer.Services.Interfaces;

namespace WeatherApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
             services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<CityValidator>();
                fv.LocalizationEnabled = false;
            }); ;
           
            services.AddSwaggerGen();

            services.AddHttpClient();

            services.AddHttpContextAccessor();

            var setting = new SettingService(Configuration.GetValue<string>("WeatherApiKey"));
            services.AddSingleton(i => setting);

            services.AddDbContext<WeatherContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient);

            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();

            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICityRepository, CityRepository>();

            services.AddTransient<ITemperatureInfoService, TemperatureInfoService>();

            services.AddTransient<IVisibilityInfoService, VisibilityInfoService>();

            services.AddTransient<IPressureInfoService, PressureInfoService>();

            services.AddTransient<IHumidityInfoService, HumidityInfoService>();

            services.AddTransient<IAPIWeatherProvider, APIWeatherProvider>();

            services.AddTransient<WeatherHelper>();

            services.AddTransient<StatisticalInfoHelper>();

            services.AddTransient<CasheHelper>();

            services.AddScoped<IValidator<CityDto>, CityValidator>();

            services.AddScoped<IValidator<WeatherConditionDto>, WeatherConditionValidator>();

            services.ConfigureMapper();

            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
