using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Repositories.Interfases;
using WeatherApp.DomainLayer.Services.Interfaces;

namespace WeatherApp.DomainLayer.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly WeatherContext _context;
        private readonly IValidator<CityDto> _validator;
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public CityService(WeatherContext context, IValidator<CityDto> validator, IMapper mapper, ICityRepository cityRepository)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task CreateCity(CityDto city)
        {
            if (GetCityByCityName(city.Name) == null)
            {
                var result = _validator.Validate(city);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
               
                await _cityRepository.CreateCity(_mapper.Map<City>(city));
            }
           
        }

        public async Task DeleteCity(int id)
        {
          
            var model = await GetCityByCityId(id);
            if (model != null)
            {
                await _cityRepository.DeleteCity(model);

            }
            else
            {
                throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
            }
        }

        public async Task<City> GetCityByCityId(int id)
        {
        return await _cityRepository.GetCityByCityId(id);
        }

        public City GetCityByCityName(string cityName)
        {
            var model = _context.Cities.Where(c => c.Name == cityName).FirstOrDefault();
            return model;
        }

    }
}
