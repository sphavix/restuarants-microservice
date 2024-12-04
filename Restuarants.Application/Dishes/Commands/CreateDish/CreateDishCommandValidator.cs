using FluentValidation;

namespace Restuarants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dish => dish.Price).GreaterThanOrEqualTo(0).WithMessage("The price must be greater than 0");

            RuleFor(dish => dish.Calories).GreaterThanOrEqualTo(0).WithMessage("The calories must be greater than 0");
        }
    }
}
