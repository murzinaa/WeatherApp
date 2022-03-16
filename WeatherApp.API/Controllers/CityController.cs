using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Services.Interfaces;

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
        public async Task<IActionResult> DeleteCity([FromRoute] int id) 
        {
            try
            {
                await _cityService.DeleteCity(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            try
            {
                var city = new CityDto
                {
                    Name = cityName 
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _cityService.CreateCity(city);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
