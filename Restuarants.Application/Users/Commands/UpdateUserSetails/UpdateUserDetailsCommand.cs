using MediatR;

namespace Restuarants.Application.Users.Commands.UpdateUserSetails
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
