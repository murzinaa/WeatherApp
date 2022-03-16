﻿using AutoMapper;
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
        private readonly IStatisticalInfoService _statisticalInfoService;
        private readonly SettingService _settingService;
        private readonly WeatherHelper _weatherHelper;
        private readonly IMemoryCache _memoryCache;

        public WeatherController(IWeatherService weatherService, IMapper mapper, ICityService cityService, IStatisticalInfoService statisticalInfoService, SettingService settingService, WeatherHelper weatherHelper, IMemoryCache memoryCache)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cityService = cityService;
            _statisticalInfoService = statisticalInfoService;
            _settingService = settingService;
            _weatherHelper = weatherHelper;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("addTemperature")]
        public async Task<IActionResult> AddTemperature(string cityName, double degrees, string dateTime = null)
        {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                var temperature = new TemperatureDto
                {
                    CityId = cityId,
                    Degrees = degrees,
                    DateTime = _weatherHelper.GetDateTime(dateTime)
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
            catch (Exception e)
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

                var temp = new TemperatureDto { Id = id, CityId = cityId, Degrees = degrees, DateTime = _weatherHelper.GetDateTime(dateTime) };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _weatherService.UpdateWeatherCondition(_mapper.Map<TemperatureDto>(temp));

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("archive")]
        public async Task Archive(int id) => await _weatherService.ArchiveWeatherCondition(id);

        [HttpGet("getHistory")]
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

                        List<Temperature> result = _weatherService.GetWeatherHistory(city);
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

                            var currentWeatherInfo = await _weatherService.GetCurrentWeather
                                (WeatherApiUrls.ReturnUrl(cityName, _settingService.ApiKey), id);

                            model = new StatisticalInfoModel
                            {
                                id = id,
                                CityName = cityName,
                                CurrentTemperature = currentWeatherInfo.MainInfo.Temp,
                                AverageTemperature = _statisticalInfoService.GetAverage(id),
                                MinTemperature = _statisticalInfoService.GetMin(id),
                                MaxTemperature = _statisticalInfoService.GetMax(id)
                            };

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
