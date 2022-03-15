using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.DomainLayer.Interfaces
{
    public interface ICityService
    {
        Task CreateCity(City city);
        Task DeleteCity(int id);
        City GetCityByCityName(string cityName);
        Task<City> GetCityByCityId(int id);
    }

}
