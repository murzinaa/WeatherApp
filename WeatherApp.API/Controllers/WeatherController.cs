using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.APIProviders;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Constants;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly IStatisticalInfoService _statisticalInfoService;
        private readonly SettingService _settingService;
        private readonly IAPIWeatherProvider _apiWeatherProvider;

        public WeatherController(IWeatherService weatherService, IMapper mapper, ICityService cityService, IStatisticalInfoService statisticalInfoService, SettingService settingService, IAPIWeatherProvider apiWeatherProvider)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cityService = cityService;
            _statisticalInfoService = statisticalInfoService;
            _settingService = settingService;
            _apiWeatherProvider = apiWeatherProvider;
        }

        [HttpPost]
        [Route("addTemperature")]
    //    var city = new CityDto { Name = cityName };
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //}
    //var dto = _mapper.Map<CityDto>(city);
    //await _cityService.CreateCity(dto);
    //        return Ok();
    public async Task<IActionResult> AddTemperature(string cityName, double degrees) 
    {
        await _cityService.CreateCity(new CityDto { Name = cityName });
        var cityId = _cityService.GetCityByCityName(cityName).Id;
        var temperature = new TemperatureDto {CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var dto = _mapper.Map<TemperatureDto>(temperature);
            await _weatherService.CreateWeatherCondition(_mapper.Map<TemperatureDto>(temperature));
            return Ok();
        }


            //var validator = new TemperatureValidator();
            //// тут чет сделала
            //await _cityService.CreateCity(new CityDto { Name = cityName });
            //var cityId = _cityService.GetCityByCityName(cityName).Id;

            //var model = new Temperature { CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now };
            //var result = validator.Validate(model);
            //if (result.IsValid)
            //{
            //    await _weatherService.CreateWeatherCondition(model);
            //    return Ok();
            //}
            //else
            //    return BadRequest(result.Errors);

        

        [HttpDelete("delete/{id}")]
        public async Task DeleteTemperature(int id)
        {
            await _weatherService.DeleteWeatherCondition(id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTemperature(int id, string cityName, double degrees)
        {
            await _cityService.CreateCity(new CityDto { Name = cityName });
            var cityId = _cityService.GetCityByCityName(cityName).Id;

            var temp = new TemperatureDto { Id = id, CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _weatherService.UpdateWeatherCondition(_mapper.Map<TemperatureDto>(temp));

            return Ok();
        }

        [HttpGet("getHistory")]
        public WeatherInfoModel GetTemperatureHistory(string city)
        {
            List<Temperature> result = _weatherService.GetWeatherHistory(city);
            WeatherInfoModel resModel = new WeatherInfoModel 
            { 
                CityId = result[0].CityId, 
                CityName = result[0].City.Name
            };
            var infoModel = _mapper.Map<WeatherInfoModel>(resModel);
            infoModel.WeatherInfo = _mapper.Map<List<WeatherModel>>(result);
            return infoModel;

        }

        [HttpPut("archive")]
        public async Task Archive(int id) => await _weatherService.ArchiveWeatherCondition(id);

        [HttpGet("statisticalInfo")]
        public async Task<StatisticalInfoModel> StatisticalInfo(string cityName)
        {
            if ( _cityService.GetCityByCityName(cityName) == null)
            {
                throw new NotFoundException(Constants.ExceptionMessages.City.NotFoundException);


            }
            else
            {
                int id = _cityService.GetCityByCityName(cityName).Id;
                var currentWeatherInfo = await _weatherService.GetCurrentWeather
                    (WeatherApiUrls.ReturnUrl(cityName, _settingService.ApiKey), id);

                StatisticalInfoModel model = new StatisticalInfoModel
                {
                    id = id,
                    CityName = cityName,
                    CurrentTemperature = currentWeatherInfo.MainInfo.Temp,
                    AverageTemperature = _statisticalInfoService.GetAverage(id),
                    MinTemperature = _statisticalInfoService.GetMin(id),
                    MaxTemperature = _statisticalInfoService.GetMax(id)
                };
                return model;
            }




        }
    }
}
