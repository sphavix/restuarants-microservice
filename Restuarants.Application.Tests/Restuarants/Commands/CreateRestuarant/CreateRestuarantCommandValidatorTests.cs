using FluentValidation.TestHelper;
using Xunit;

namespace Restuarants.Application.Restuarants.Commands.CreateRestuarant.Tests
{
    public class CreateRestuarantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestuarantCommand()
            {
                Name = "Test",
                Description = "Test Description",
                Category = "Italian",
                ContactEmail = "test@test.com",
                ContactNumber = "1234567890",
                PostalCode = "1234",
            };

            var validator = new CreateRestuarantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForValidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestuarantCommand()
            {
                Name = "Test",
                Description = "Test Description",
                Category = "Italies",
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                PostalCode = "234",
            };

            var validator = new CreateRestuarantCommandValidator();

            //act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Category);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
            result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
        }

        [Theory]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("American")]
        [InlineData("South African")]
        [InlineData("Indian")]
        [InlineData("Street Food")]
        public void Validor_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange
            var validator = new CreateRestuarantCommandValidator();

            var command = new CreateRestuarantCommand { Category = category };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        [Theory]
        [InlineData("2")]
        [InlineData("23")]
        [InlineData("234")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            // arrange
            var validator = new CreateRestuarantCommandValidator();

            var command = new CreateRestuarantCommand { PostalCode = postalCode };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }
    }
}