using FluentValidation;
using Restuarants.Application.Restuarants.Dtos;

namespace Restuarants.Application.Restuarants.Validators
{
    public class CreateRestuarantDtoValidator : AbstractValidator<CreateRestuarantDto>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "American", "South African", "Indian"];
        public CreateRestuarantDtoValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100).NotEmpty().WithMessage("The restuarant name is required");

            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required!");

            RuleFor(dto => dto.Category).Must(validCategories.Contains).WithMessage("Invalid category. Please specify a valid category");

            RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(dto => dto.ContactEmail).Length(10).WithMessage("Contact Number must be 10 digits");

            RuleFor(dto => dto.PostalCode).Length(4).WithMessage("Postal code must be 4 digits");
        }
    }
}
