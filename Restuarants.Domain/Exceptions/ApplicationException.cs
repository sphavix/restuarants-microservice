using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Domain.Exceptions
{
    public class ApplicationException(string message) : Exception(message)
    {

    }
}
