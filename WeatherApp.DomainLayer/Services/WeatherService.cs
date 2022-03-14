using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.APIProviders;
using WeatherApp.APIProviders.Models;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class WeatherService : IWeatherService

    {
        private readonly IAPIWeatherProvider _apiWeatherProvider;

        public WeatherService(IAPIWeatherProvider apiWeatherProvider)
        {
            _apiWeatherProvider = apiWeatherProvider;
        }
        public async Task<WeatherResult> GetCurrentWeatherByCity(string url)
        {
            return await _apiWeatherProvider.GetCurrentWeather(url);
        }
    }
}
