using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Exceptions
{
    public sealed class DatabaseException : TechnicalException
    {
        public DatabaseException(string message , Exception inner) : base(message , inner) { }
    }
}
