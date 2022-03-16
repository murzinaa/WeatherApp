using AutoMapper;
using FluentValidation;
using System;
using System.Threading.Tasks;
using WeatherApp.APIProviders;
using WeatherApp.APIProviders.Models;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Interfaces;
using WeatherApp.DomainLayer.Repositories.Interfases;

namespace WeatherApp.DomainLayer.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IAPIWeatherProvider _apiWeatherProvider;
        private readonly IValidator<WeatherConditionDto> _validator;
        private readonly IMapper _mapper;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IAPIWeatherProvider apiWeatherProvider, IMapper mapper, IValidator<WeatherConditionDto> validator, IWeatherRepository weatherRepository)
        {
            _apiWeatherProvider = apiWeatherProvider;
            _mapper = mapper;
            _validator = validator;
            _weatherRepository = weatherRepository;
        }

        public async Task ArchiveWeatherCondition(int id)
        {
            await _weatherRepository.ArchiveWeatherCondition(id);

        }

        public async Task CreateWeatherCondition(WeatherConditionDto weatherCondition)
        {
            var result = _validator.Validate(weatherCondition);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _weatherRepository.CreateWeatherCondition(_mapper.Map<WeatherCondition>(weatherCondition));
        }

        // потом решить шо и как
        public async Task UpdateWeatherCondition(WeatherConditionDto weatherCondition)
        {
            //var temp = await _weatherRepository.GetWeatherCondition(weatherCondition.Id);
            var temp = _weatherRepository.GetFirstWeatherCondition(weatherCondition.Id);
                
                //_context.WeatherConditions.Where(t => t.Id == weatherCondition.Id).ToList().FirstOrDefault();

            if (temp != null)
            {
                var result = _validator.Validate(weatherCondition);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
                await _weatherRepository.UpdateWeatherCondition(weatherCondition, temp);

            }
            else
            {
                throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);
            }
        }
        public async Task DeleteWeatherCondition(int id)
        {
            var model = await _weatherRepository.GetWeatherCondition(id);

            if (model != null)
            {
                await _weatherRepository.DeleteWeatherCondition(model);
            }
            else
            {
                throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);
            }
        }

        public async Task<WeatherResult> GetCurrentWeather(string url, int id)
        {
            var result = await _apiWeatherProvider.GetCurrentWeather(url);
            await CreateWeatherCondition(new WeatherConditionDto
            {
                CityId = id,
                DateTime = DateTime.Now,
                Degrees = result.MainInfo.Temp,
                Humidity = result.MainInfo.Humidity,
                Pressure = result.MainInfo.Pressure
            });

            return result;
        }


        public City GetWeatherHistory(string CityName)
        {
            var city = _weatherRepository.GetWeatherHistory(CityName);

            if (city != null)
            {
                return city;

            }
            else
            {
                throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            }
        }

    }

}

