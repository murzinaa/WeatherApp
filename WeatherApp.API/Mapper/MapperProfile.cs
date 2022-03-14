using AutoMapper;
using WeatherApp.API.Models;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Temperature, WeatherModel>();
        }
    }
}
