﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.DomainLayer.DTOs;
using WeatherApp.DomainLayer.Interfaces;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public async Task DeleteCity([FromRoute] int id) => await _cityService.DeleteCity(id);


        [HttpPost("create/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            var city = new CityDto { Name = cityName };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = _mapper.Map<CityDto>(city);
            await _cityService.CreateCity(dto);
            return Ok();
            ////var city = new CreateCityModel { Name = cityName };
            //CreateCityValidator validator = new CreateCityValidator();

            //ValidationResult results = validator.Validate(city);
            //if (results.IsValid)
            //{
            //    var model = _mapper.Map<City>(city);
            //    await _cityService.CreateCity(model);
            //    return Ok();
            //}
            //else
            //    return BadRequest(results.Errors);

        }
    }
}
