using FluentValidation;
using WeatherApp.DomainLayer.DTOs;

namespace WeatherApp.DomainLayer.Validation
{
    public class WeatherConditionValidator : AbstractValidator<WeatherConditionDto>
    {
        public WeatherConditionValidator()
        {
            RuleFor(w => w.Degrees).GreaterThan(-50).LessThan(50).WithMessage("The temperature should be between -50 and 50 Celsius degrees");
            RuleFor(w => w.Visibility).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithMessage("Visibility should be between 0 and 100%");
            RuleFor(w => w.Humidity).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100).WithMessage("Humidity should be between 0 and 100%");
        }
    }
}
