using System.Threading.Tasks;
using WeatherApp.DomainLayer.Constants;
using WeatherApp.DomainLayer.Services.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.API.Helpers
{
    public class StatisticalInfoHelper
    {
        private readonly ITemperatureInfoService _statisticalInfoService;
        private readonly IVisibilityInfoService _visibilityInfoService;
        private readonly IPressureInfoService _pressureInfoService;
        private readonly IHumidityInfoService _humidityInfoService;
        private readonly IWeatherService _weatherService;
        private readonly SettingService _settingService;


        public StatisticalInfoHelper(ITemperatureInfoService statisticalInfoService, IVisibilityInfoService visibilityInfoService, IPressureInfoService pressureInfoService, IHumidityInfoService humidityInfoService, IWeatherService weatherService, SettingService settingService)
        {
            _statisticalInfoService = statisticalInfoService;
            _visibilityInfoService = visibilityInfoService;
            _pressureInfoService = pressureInfoService;
            _humidityInfoService = humidityInfoService;
            _weatherService = weatherService;
            _settingService = settingService;
        }


        public async Task<StatisticalInfoModel> GetStatisticalInfo(int id, string cityName)
        {
            var currentWeatherInfo = await _weatherService.GetCurrentWeather
                                (WeatherApiUrls.ReturnUrl(cityName, _settingService.ApiKey), id);

            var temp = new TemperatureInfoModel
            {
                CurrentTemperature = currentWeatherInfo.MainInfo.Temp,
                AverageTemperature = _statisticalInfoService.GetAverageTemperature(id),
                MinTemperature = _statisticalInfoService.GetMinTemperature(id),
                MaxTemperature = _statisticalInfoService.GetMaxTemperature(id)
            };

            var vis = new VisibilityInfoModel
            {
                CurrentVisibility = currentWeatherInfo.Visibility,
                AverageVisibility = _visibilityInfoService.GetAverageVisibility(id),
                MinVisibility = _visibilityInfoService.GetMinVisibility(id),
                MaxVisibility = _visibilityInfoService.GetMaxVisibility(id)
            };

            var hum = new HumidityInfoModel
            {
                CurrentHumidity = currentWeatherInfo.MainInfo.Humidity,
                AverageHumidity = _humidityInfoService.GetAverageHumidity(id),
                MinHumidity = _humidityInfoService.GetMinHumidity(id),
                MaxHumidity = _humidityInfoService.GetMaxHumidity(id)
            };

            var pres = new PressureInfoModel
            {
                CurrentPressure = currentWeatherInfo.MainInfo.Pressure,
                AveragePressure = _pressureInfoService.GetAveragePressure(id),
                MinPressure = _pressureInfoService.GetMinPressure(id),
                MaxPressure = _pressureInfoService.GetMaxPressure(id)
            };
            var model = new StatisticalInfoModel
            {
                id = id,
                CityName = cityName,
                Temperature = temp,
                Visibility = vis,
                Humidity = hum,
                Pressure = pres
            };

            return model;
        }
    }
}
