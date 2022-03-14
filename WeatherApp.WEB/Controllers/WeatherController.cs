using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.APIProviders;

namespace WeatherApp.WEB.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IAPIWeatherProvider _apiWeatherProvider;

        public WeatherController(IAPIWeatherProvider apiWeatherProvider)
        {
            _apiWeatherProvider = apiWeatherProvider;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _apiWeatherProvider.GetCurrentWeather("https://api.openweathermap.org/data/2.5/weather?q=Lviv&units=metric&appid=7e8552375a637ff69f6c02eba7a90032"));
        }
    }
}
