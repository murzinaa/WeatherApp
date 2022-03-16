using AutoMapper;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.Models;

namespace WeatherApp.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CityDto, City>();
            CreateMap<City, CityDto>();

            CreateMap<WeatherConditionDto, WeatherCondition>();
            CreateMap<WeatherCondition, WeatherInfoModel>();
            CreateMap<WeatherCondition, WeatherModel>();
        }
    }
}
