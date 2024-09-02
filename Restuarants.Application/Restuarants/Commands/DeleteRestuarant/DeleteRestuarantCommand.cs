using MediatR;

namespace Restuarants.Application.Restuarants.Commands.DeleteRestuarant
{
    public class DeleteRestuarantCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteRestuarantCommand(Guid id) 
        {
            Id = id;
        }    
    }
}
