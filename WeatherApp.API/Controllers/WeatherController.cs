using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.API.Helpers;
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
        private readonly WeatherHelper _weatherHelper;
        private readonly StatisticalInfoHelper _statisticalInfoHelper;
        private readonly IMemoryCache _memoryCache;

        public WeatherController(IWeatherService weatherService, IMapper mapper, ICityService cityService, WeatherHelper weatherHelper, IMemoryCache memoryCache, StatisticalInfoHelper statisticalInfoHelper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cityService = cityService;
            _weatherHelper = weatherHelper;
            _memoryCache = memoryCache;
            _statisticalInfoHelper = statisticalInfoHelper;
        }

        [HttpPost]
        [Route("createWeatherCondition")]
        public async Task<IActionResult> CreateWeatherCondition(string cityName, double degrees, double pressure, double visibility, double humidity, string dateTime = null)
        {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                var weatherCondition = new WeatherConditionDto
                {
                    CityId = cityId,
                    Degrees = degrees,
                    DateTime = _weatherHelper.GetDateTime(dateTime),
                    Pressure = pressure,
                    Humidity = humidity,
                    Visibility = visibility
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _weatherService.CreateWeatherCondition(_mapper.Map<WeatherConditionDto>(weatherCondition));

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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTemperature(int id, string cityName, double degrees, double pressure, double visibility, double humidity, string dateTime = null)
        {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                var weatherCondition = new WeatherConditionDto { Id = id, CityId = cityId, Degrees = degrees,Pressure = pressure, Humidity = humidity, Visibility = visibility, DateTime = _weatherHelper.GetDateTime(dateTime) };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _weatherService.UpdateWeatherCondition(_mapper.Map<WeatherConditionDto>(weatherCondition));

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("archive/{id}")]
        public async Task Archive(int id) => await _weatherService.ArchiveWeatherCondition(id);

        [HttpGet("getHistory/{city}")]
        //WeatherInfoModel
        public IActionResult GetTemperatureHistory(string city)
        {


            WeatherInfoModel resModel = new WeatherInfoModel();

            if (!_memoryCache.TryGetValue($"WeatherList_{city}", out resModel))
            {
                if (resModel == null)
                {
                    try
                    {

                        List<WeatherCondition> result = _weatherService.GetWeatherHistory(city);
                        resModel = new WeatherInfoModel()
                        {
                            CityId = result[0].CityId,
                            CityName = result[0].City.Name
                        };

                        var infoModel = _mapper.Map<WeatherInfoModel>(resModel);
                        infoModel.WeatherInfo = _mapper.Map<List<WeatherModel>>(result);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                _memoryCache.Set($"WeatherList_{city}", resModel, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }
            return Ok(resModel);

        }



        [HttpGet("statisticalInfo")]
        public async Task<IActionResult> StatisticalInfo(string cityName)
        {
            StatisticalInfoModel model = new StatisticalInfoModel();

            if (!_memoryCache.TryGetValue($"StatisticalInfo_{cityName}", out model))
            {
                if (model == null)
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
                            model = await _statisticalInfoHelper.GetStatisticalInfo(id, cityName);

                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                _memoryCache.Set($"StatisticalInfo_{cityName}", model, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                });

            }

            return Ok(model);

        }
    }
}
