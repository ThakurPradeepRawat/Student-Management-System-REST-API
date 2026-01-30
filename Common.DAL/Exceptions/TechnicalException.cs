using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Exceptions
{
    public abstract class TechnicalException : Exception
    {
        protected TechnicalException(string Message, Exception inner = null) : base(Message, inner) { }

    }
}
