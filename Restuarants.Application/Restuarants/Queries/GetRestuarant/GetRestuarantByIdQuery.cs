using MediatR;
using Restuarants.Application.Restuarants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Application.Restuarants.Queries.GetRestuarant
{
    public class GetRestuarantByIdQuery : IRequest<RestuarantDto>
    {
        public Guid Id { get; set; }

        public GetRestuarantByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
