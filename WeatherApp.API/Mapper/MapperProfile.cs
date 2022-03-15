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
            CreateMap<TemperatureDto, Temperature>();



            //CreateMap<WeatherApp.DataLayer.Entities.Temperature, WeatherApp.Models.TemperatureModel>()
            //   ;
            CreateMap<Temperature, WeatherInfoModel>();
            CreateMap<Temperature, WeatherModel>();
            //CreateMap<City, CityModel>();
            
            //CreateMap<Temperature, StatisticalInfoModel>();
                //.ForMember(s => s.Average, opt => opt.MapFrom(c => ));
        }
    }
}
