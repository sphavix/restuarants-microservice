﻿using MediatR;
using Restuarants.Application.Restuarants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Application.Restuarants.Commands.CreateRestuarant
{
    public class CreateRestuarantCommand : IRequest<RestuarantDto>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
