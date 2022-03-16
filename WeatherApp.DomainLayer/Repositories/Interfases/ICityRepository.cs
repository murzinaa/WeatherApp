using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Entities;

namespace WeatherApp.DomainLayer.Repositories.Interfases
{
    public interface ICityRepository
    {
        Task CreateCity(City city);
        Task DeleteCity(City city);
        Task<City> GetCityByCityId(int id);
        Task<City> GetCityByCityName(string cityName);

    }
}
