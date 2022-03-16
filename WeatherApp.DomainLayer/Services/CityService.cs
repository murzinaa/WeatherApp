using AutoMapper;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.DataLayer;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Exeptions;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.DomainLayer.Services
{
    public class CityService : ICityService
    {
        private readonly WeatherContext _context;
        private readonly IValidator<CityDto> _validator;
        private readonly IMapper _mapper;

        public CityService(WeatherContext context, IValidator<CityDto> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
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
                var cityRes = _mapper.Map<City>(city);
                await _context.Cities.AddAsync(cityRes);
                await _context.SaveChangesAsync();
            }
            else
                throw new System.Exception(Constants.Constants.ExceptionMessages.City.CityAlreadyCreated);
        }

        public async Task DeleteCity(int id)
        {
            var model = await _context.Set<City>().FindAsync(id);
            if (model != null)
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException(Constants.Constants.ExceptionMessages.City.NotFoundException);
        }

        public async Task<City> GetCityByCityId(int id)
        {
            var model = await _context.Set<City>().FindAsync(id);
            return model;
        }

        public City GetCityByCityName(string cityName)
        {
            var model = _context.Cities.Where(c => c.Name == cityName).FirstOrDefault();
            return model;
        }

    }
}
