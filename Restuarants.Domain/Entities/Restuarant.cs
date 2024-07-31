﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Domain.Entities
{
    public class Restuarant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public Address? Address { get; set; }
        public List<Dish> Dishes { get; set; } = new();
    }
}
