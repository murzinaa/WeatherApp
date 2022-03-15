using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface ICityService
    {
        Task CreateCity(CityDto city);
        Task DeleteCity(int id);
        City GetCityByCityName(string cityName);
        Task<City> GetCityByCityId(int id);
    }

}
