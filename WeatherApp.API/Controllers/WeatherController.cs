using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.Constants;
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

        public WeatherController(IWeatherService weatherService, IMapper mapper, ICityService cityService, IStatisticalInfoService statisticalInfoService)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _cityService = cityService;
            _statisticalInfoService = statisticalInfoService;
        }

        [HttpPost]
        [Route("addTemperature")]
        public async Task AddTemperature(string cityName, double degrees)
        {
            
            await _cityService.CreateCity(new City { Name = cityName });
            var cityId = _cityService.GetCityByCityName(cityName).Id;
            var model = new Temperature { CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now };
            await _weatherService.CreateWeatherCondition(model);
            
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteTemperature(int id)
        {
            await _weatherService.DeleteWeatherCondition(id);
        }

        [HttpPut("update")]
        public async Task UpdateTemperature(int id, string cityName, double degrees)
        {
            await _cityService.CreateCity(new City { Name = cityName });
            var cityId = _cityService.GetCityByCityName(cityName).Id;
            await _weatherService.UpdateWeatherCondition(new Temperature {Id = id, CityId = cityId, Degrees = degrees, DateTime = System.DateTime.Now });   
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
        public async Task<StatisticalInfoModel> StatisticalInfo(int id)
        {
            if (await _cityService.GetCityByCityId(id) == null)
            {
                throw new NotFoundException(Constants.ExceptionMessages.City.NotFoundException);


            }
            else
            {
                StatisticalInfoModel model = new StatisticalInfoModel
                {
                    id = id,
                    AverageTemperature = _statisticalInfoService.GetAverage(id),
                    MinTemperature = _statisticalInfoService.GetMin(id),
                    MaxTemperature = _statisticalInfoService.GetMax(id)
                };
                return model;
            }




        }
    }
}
