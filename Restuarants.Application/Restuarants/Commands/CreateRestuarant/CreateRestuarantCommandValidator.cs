using FluentValidation;

namespace Restuarants.Application.Restuarants.Commands.CreateRestuarant
{
    public class CreateRestuarantCommandValidator : AbstractValidator<CreateRestuarantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "American", "South African", "Indian", "Street Food"];
        public CreateRestuarantCommandValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100).NotEmpty().WithMessage("The restuarant name is required");

            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required!");

            RuleFor(dto => dto.Category).Must(validCategories.Contains).WithMessage("Invalid category. Please specify a valid category");

            RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(dto => dto.ContactNumber).Length(10).WithMessage("Contact Number must be 10 digits");

            RuleFor(dto => dto.PostalCode).Length(4).WithMessage("Postal code must be 4 digits");
        }
    }
}
