using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.API.Models;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost]
        public async Task AddTemperature(int cityId, double degrees)
        {
            var model = new Temperature { CityId=cityId, Degrees=degrees, DateTime = System.DateTime.Now};
            await _weatherService.CreateWeatherCondition(model);
        }

        [HttpDelete]
        public async Task DeleteTemperature(int id)
        {
            await _weatherService.DeleteWeatherCondition(id);
        }

        [HttpPut]
        public async Task UpdateTemperature(int cityId, double degrees, int id)
        {
            await _weatherService.UpdateWeatherCondition(new Temperature {Id = id, CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now });   
        }

        [HttpGet]
        public WeatherModel GetTemperature(string city)
        {
            var res = _weatherService.GetWeatherHistory(city);
            var model = _mapper.Map<WeatherModel>(res);
            //var json = JsonSerializer.Serialize(res);
            return model;
        }
    }
}
