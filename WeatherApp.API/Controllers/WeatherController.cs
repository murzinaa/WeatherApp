using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.API.Helpers;
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
        private readonly WeatherHelper _weatherHelper;

        public WeatherController(IWeatherService weatherService, IMapper mapper, ICityService cityService, IStatisticalInfoService statisticalInfoService, SettingService settingService, IAPIWeatherProvider apiWeatherProvider, WeatherHelper weatherHelper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cityService = cityService;
            _statisticalInfoService = statisticalInfoService;
            _settingService = settingService;
            _apiWeatherProvider = apiWeatherProvider;
            _weatherHelper = weatherHelper;
        }

        [HttpPost]
        [Route("addTemperature")]
    public async Task<IActionResult> AddTemperature(string cityName, double degrees, string dateTime = null) 
    {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                //DateTime date;

                //if (dateTime == null)
                //    date = DateTime.Now;
                //else
                //    date = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                //                       System.Globalization.CultureInfo.InvariantCulture);
               
                var temperature = new TemperatureDto { CityId = cityId, Degrees = degrees, DateTime = _weatherHelper.GetDateTime(dateTime) };
            
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //var dto = _mapper.Map<TemperatureDto>(temperature);

                await _weatherService.CreateWeatherCondition(_mapper.Map<TemperatureDto>(temperature));

                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }       

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTemperature(int id)
        {
            try
            {
                await _weatherService.DeleteWeatherCondition(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTemperature(int id, string cityName, double degrees, string dateTime = null)
        {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                //DateTime date;

                //if (dateTime == null)
                //    date = DateTime.Now;
                //else
                //    date = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                //                       System.Globalization.CultureInfo.InvariantCulture);

                var temp = new TemperatureDto { Id = id, CityId = cityId, Degrees = degrees, DateTime = _weatherHelper.GetDateTime(dateTime) };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _weatherService.UpdateWeatherCondition(_mapper.Map<TemperatureDto>(temp));

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getHistory")]
        //WeatherInfoModel
        public IActionResult GetTemperatureHistory(string city)
        {
            try
            {
                List<Temperature> result = _weatherService.GetWeatherHistory(city);
                WeatherInfoModel resModel = new WeatherInfoModel
                {
                    CityId = result[0].CityId,
                    CityName = result[0].City.Name
                };
                var infoModel = _mapper.Map<WeatherInfoModel>(resModel);
                infoModel.WeatherInfo = _mapper.Map<List<WeatherModel>>(result);
                return Ok(infoModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("archive")]
        public async Task Archive(int id) => await _weatherService.ArchiveWeatherCondition(id);

        [HttpGet("statisticalInfo")]
        public async Task<IActionResult> StatisticalInfo(string cityName)
        {
            try
            {
                if (_cityService.GetCityByCityName(cityName) == null)
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
                    return Ok(model);
                }
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }




        }
    }
}
