using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.BAL.Exceptions.Base
{
    public abstract  class BusinessException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected BusinessException(string message , int Statuscode , string Errorcode):base(message) {
            StatusCode = Statuscode;
            ErrorCode = Errorcode;

        }

    }
}
