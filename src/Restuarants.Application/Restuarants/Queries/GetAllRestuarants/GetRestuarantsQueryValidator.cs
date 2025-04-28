using FluentValidation;
using Restuarants.Application.Restuarants.Dtos;

namespace Restuarants.Application.Restuarants.Queries.GetAllRestuarants
{
    public class GetRestuarantsQueryValidator : AbstractValidator<GetRestuarantsQuery>
    {
        private int[] allowedPageSize = [5, 10, 15, 30];
        private string[] allowedSortByColumns = [nameof(RestuarantDto.Name), nameof(RestuarantDto.Category), nameof(RestuarantDto.Description)];
        public GetRestuarantsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .Must(value => allowedPageSize.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowedPageSize)}]");

            RuleFor(x => x.SortBy)
               .Must(value => allowedSortByColumns.Contains(value))
               .When(q => q.SortBy != null)
               .WithMessage($"Sorting is optional, but must be in [{string.Join(",", allowedSortByColumns)}]");



        }
    }
}
