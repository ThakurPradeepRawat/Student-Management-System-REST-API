using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Exceptions 
{
    sealed class EmailException: TechnicalException
    {
        public EmailException(string Message, Exception inner = null) : base(Message, inner)
        {
        }
    }
}
