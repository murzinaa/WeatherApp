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
            CreateMap<WeatherConditionDto, WeatherCondition>();



            //CreateMap<WeatherApp.DataLayer.Entities.Temperature, WeatherApp.Models.TemperatureModel>()
            //   ;
            CreateMap<WeatherCondition, WeatherInfoModel>();
            CreateMap<WeatherCondition, WeatherModel>();
            //CreateMap<City, CityModel>();
            
            //CreateMap<Temperature, StatisticalInfoModel>();
                //.ForMember(s => s.Average, opt => opt.MapFrom(c => ));
        }
    }
}
