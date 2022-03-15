using FluentValidation;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Validation
{
    public class TemperatureValidator : AbstractValidator<TemperatureDto>
    {
        public TemperatureValidator()
        {
            RuleFor(t => t.Degrees).GreaterThan(-50).LessThan(50).WithMessage("The temperature should be between -50 and 50 Celsius degrees");
            //RuleFor(t => t.City.Name).Length(4, 30).WithMessage("The City Name cannot be more that 30 characters and less that 4 characters");
        }
    }
}
