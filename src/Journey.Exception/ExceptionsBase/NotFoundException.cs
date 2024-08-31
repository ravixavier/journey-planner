using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}