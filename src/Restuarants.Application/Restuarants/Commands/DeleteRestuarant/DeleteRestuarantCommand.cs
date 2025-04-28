using MediatR;

namespace Restuarants.Application.Restuarants.Commands.DeleteRestuarant
{
    public class DeleteRestuarantCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteRestuarantCommand(int id) 
        {
            Id = id;
        }    
    }
}
