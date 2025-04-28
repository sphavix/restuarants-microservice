using FluentValidation;

namespace Restuarants.Application.Restuarants.Commands.UpdateRestuarant
{
    public class UpdateRestuarantCommandValidator : AbstractValidator<UpdateRestuarantCommand>
    {
        public UpdateRestuarantCommandValidator()
        {
            RuleFor(c => c.Name)
                .Length(3, 100);
        }
    }
}
