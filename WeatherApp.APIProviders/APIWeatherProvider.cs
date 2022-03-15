using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherApp.APIProviders.Models;

namespace WeatherApp.APIProviders
{
    public class APIWeatherProvider: IAPIWeatherProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        public APIWeatherProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task<WeatherResult> GetCurrentWeather(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            WeatherResult model = JsonSerializer.Deserialize<WeatherResult>(content);

            return model;
        }
    }
}
