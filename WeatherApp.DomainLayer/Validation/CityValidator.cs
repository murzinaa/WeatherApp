using FluentValidation;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Validation
{
    public class CityValidator : AbstractValidator<CityDto>
    {

        public CityValidator()
        {
            RuleFor(c => c.Name).Length(4, 30).WithMessage("The City Name cannot be more that 30 characters and less that 4 characters");
        }
    }
}
