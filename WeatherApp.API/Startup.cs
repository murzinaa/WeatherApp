using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherApp.DataLayer;
using WeatherApp.DomainLayer.Services;
using WeatherApp.DomainLayer.Interfaces;
using WeatherApp.API.Extensions;
using WeatherApp.APIProviders;
using FluentValidation.AspNetCore;
using FluentValidation;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Validation;
using WeatherApp.API.Helpers;

namespace WeatherApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<CityValidator>();
                fv.LocalizationEnabled = false;
            }); ;
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new ValidationFilter());
            //})
            //.AddFluentValidation(options =>
            //{
            //    options.RegisterValidatorsFromAssemblyContaining<Startup>();
            //});
            //services.ConfigureMapper();

//            services.AddControllers().AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);
            services.AddSwaggerGen();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            var setting = new SettingService(Configuration.GetValue<string>("WeatherApiKey"));
            services.AddSingleton(i => setting);

            //services.AddMvc();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Weather API",
            //        Version = "v1"
            //    });
            //});
            services.AddDbContext<WeatherContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient);
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IStatisticalInfoService, StatisticalInfoService>();
            
            services.AddTransient<IAPIWeatherProvider, APIWeatherProvider>();
            services.AddTransient<WeatherHelper>();
            services.ConfigureMapper();
            services.AddScoped<IValidator<CityDto>, CityValidator>();
            services.AddScoped<IValidator<TemperatureDto>, TemperatureValidator>();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1");

                // c.RoutePrefix = string.Empty;
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
