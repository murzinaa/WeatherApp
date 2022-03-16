using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.APIProviders;
using WeatherApp.APIProviders.Models;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherContext _context;
        private readonly IAPIWeatherProvider _apiWeatherProvider;
        private readonly IValidator<WeatherConditionDto> _validator;
        private readonly IMapper _mapper;

        public WeatherService(WeatherContext context, IAPIWeatherProvider apiWeatherProvider, IMapper mapper, IValidator<WeatherConditionDto> validator)
        {
            _context = context;
            _apiWeatherProvider = apiWeatherProvider;
            _mapper = mapper;
            _validator = validator;
          
        }

        public async Task ArchiveWeatherCondition(int id)
        {

            try
            {
                var temp = _context.WeatherConditions.Where(t => t.Id == id).ToList().First<WeatherCondition>();
                temp.IsArchieved = true;

                await _context.SaveChangesAsync();
            }
            catch
            {

                throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);
            }
            //if (temp != null)
            //{
            //    temp.IsArchieved = true;

            //    await _context.SaveChangesAsync();

            //}
            //else
            //{
            //    throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);
            //}
        }

        public async Task CreateWeatherCondition(WeatherConditionDto weatherCondition)
        {
            var result = _validator.Validate(weatherCondition);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var temp = _mapper.Map<WeatherCondition>(weatherCondition);

            await _context.AddAsync(temp);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateWeatherCondition(WeatherConditionDto weatherCondition)
        {
            var temp = _context.WeatherConditions.Where(t => t.Id == weatherCondition.Id).ToList().FirstOrDefault<WeatherCondition>();

            if (temp != null)
            {
                var result = _validator.Validate(weatherCondition);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                temp.CityId = weatherCondition.CityId;
                temp.Degrees = weatherCondition.Degrees;
                temp.DateTime = weatherCondition.DateTime;

                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);

        }
        public async Task DeleteWeatherCondition(int id)
        {
            var model = await _context.Set<WeatherCondition>().FindAsync(id);

            if (model != null)
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);
        }

        public async Task<WeatherResult> GetCurrentWeather(string url, int id)
        {
            var result = await _apiWeatherProvider.GetCurrentWeather(url);
            await CreateWeatherCondition(new WeatherConditionDto { CityId = id, DateTime = DateTime.Now, Degrees = result.MainInfo.Temp });

            return result;
        }


        public List<WeatherCondition> GetWeatherHistory(string CityName)
        {
            var city = _context.Cities.Where(c => c.Name == CityName).FirstOrDefault();

            if (city != null)
            {
                var id = city.Id;
                var temp = _context.WeatherConditions.FirstOrDefault(t => t.CityId == id);
                if (temp != null)
                {
                    var weatherForecasts = _context.WeatherConditions.Where(t => t.CityId == id).ToList();
                    //var model = _context.Cities.Where(c => c.Name == CityName).ToList();
                    return weatherForecasts;
                }
                else throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);

            }
            else
                throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
        }

    }
        //   var city = _context.Cities.Where(c => c.Name == CityName).FirstOrDefault();

        //    if (city != null)
        //    {
        //        var id = city.Id;
        //        var temp = _context.Temperature.FirstOrDefault(t => t.CityId == id);
        //        if (temp != null)
        //        {
        //            var weatherForecasts = _context.Temperature.Where(t => t.CityId == id).ToList();
        //            //var model = _context.Cities.Where(c => c.Name == CityName).ToList();
        //            return weatherForecasts;
        //        }
        //        else throw new NotFoundException(Constants.Constants.ExceptionMessages.Temperature.NotFoundException);

        //    }
        //    else
        //        throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
        //}

        //public async Task UpdateWeatherCondition(Temperature temperature)
        //{
        //   var temp = _context.Temperature.Where(t => t.Id == temperature.Id).ToList().FirstOrDefault<Temperature>();

        //    if (temp != null)
        //    {
        //        temp.CityId = temperature.CityId;
        //        temp.Degrees = temperature.Degrees;
        //        temp.DateTime = temperature.DateTime;

        //        await _context.SaveChangesAsync();

        //    }


        //}


    }

