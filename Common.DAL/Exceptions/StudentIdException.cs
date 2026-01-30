using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Exceptions
{
    sealed class StudentIdException : TechnicalException
    {
        public StudentIdException(string message, Exception inner = null) : base(message , inner) { }
    }
}
