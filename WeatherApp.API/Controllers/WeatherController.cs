using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using WeatherApp.API.Helpers;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Constants;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Services.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ICityService _cityService;
        private readonly WeatherHelper _weatherHelper;
        private readonly StatisticalInfoHelper _statisticalInfoHelper;
        private readonly CasheHelper _casheHelper;

        public WeatherController(IWeatherService weatherService, ICityService cityService, WeatherHelper weatherHelper, StatisticalInfoHelper statisticalInfoHelper, CasheHelper casheHelper)
        {
            _weatherService = weatherService;
            _cityService = cityService;
            _weatherHelper = weatherHelper;
            _statisticalInfoHelper = statisticalInfoHelper;
            _casheHelper = casheHelper;
        }



        [HttpPost]
        public async Task<IActionResult> CreateWeather(string cityName, double degrees, double pressure, double visibility, double humidity, string dateTime = null)
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

                await _weatherService.CreateWeatherCondition(weatherCondition);

                return Ok();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeather(int id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeather(int id, string cityName, double degrees, double pressure, double visibility, double humidity, string dateTime = null)
        {
            try
            {
                await _cityService.CreateCity(new CityDto { Name = cityName });
                var cityId = _cityService.GetCityByCityName(cityName).Id;

                var weatherCondition = new WeatherConditionDto 
                {
                    Id = id,
                    CityId = cityId, 
                    Degrees = degrees, 
                    Pressure = pressure, 
                    Humidity = humidity,
                    Visibility = visibility,
                    DateTime = _weatherHelper.GetDateTime(dateTime) 
                };



                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _weatherService.UpdateWeatherCondition(weatherCondition);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                await _weatherService.ArchiveWeatherCondition(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
           
        }

        [HttpGet("{city}/history")]
        public IActionResult GetWeatherHistory(string city)
        {


            WeatherInfoModel resModel = new WeatherInfoModel();

            if (!_casheHelper.GetCashe($"WeatherList_{city}", out resModel))
            {
                if (resModel == null)
                {
                    try
                    {
                        City res = _weatherService.GetWeatherHistory(city);
                        resModel = _weatherHelper.FillModel(res);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                _casheHelper.SetCashe($"WeatherList_{city}", resModel, 30);
            }
            return Ok(resModel);

        }



        [HttpGet("{city}/info")]
        public async Task<IActionResult> StatisticalInfo(string city)
        {
            StatisticalInfoModel resModel = new StatisticalInfoModel();

            if (!_casheHelper.GetCashe($"StatisticalInfo_{city}", out resModel))
            {
                if (resModel == null)
                {
                    try
                    {
                        var getCity = _cityService.GetCityByCityName(city);

                        if (getCity == null)
                        {
                            throw new  NotFoundException(Constants.ExceptionMessages.City.NotFoundException);

                        }
                        else
                        {
                            int id = getCity.Id;
                            resModel = await _statisticalInfoHelper.GetStatisticalInfo(id, city);
                        }
                    }
                    catch (Exception e)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, e.Message);
                    }
                }
                _casheHelper.SetCashe($"StatisticalInfo_{city}", resModel, 60);

            }

            return Ok(resModel);

        }
    }
}
