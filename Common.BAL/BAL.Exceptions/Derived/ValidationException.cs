using Common.BAL.BAL.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.BAL.Exceptions.Derived
{
    public sealed class ValidationException : BusinessException
    {
        public ValidationException(string message) : base(message, StatusCodes.Status400BadRequest, "BUS-400" )
        {
        }
    }
}
