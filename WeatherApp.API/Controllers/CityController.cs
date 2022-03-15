using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpDelete("{id}")]
        public async Task DeleteCity([FromRoute] int id) => await _cityService.DeleteCity(id);
        

        [HttpPost("{cityName}")]
        public async Task AddCity([FromRoute] string cityName) => await _cityService.CreateCity
            (new DataLayer.Entities.City {Name = cityName});
        
    }
}
