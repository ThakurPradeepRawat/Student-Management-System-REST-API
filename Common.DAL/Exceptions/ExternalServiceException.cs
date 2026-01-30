using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Exceptions
{
    public sealed class ExternalServiceException : TechnicalException
    {
        public ExternalServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
