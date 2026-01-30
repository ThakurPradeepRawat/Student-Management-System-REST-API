using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.DTO
{
    public sealed class ErrorResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }

        public string CorrelationId { get; set; }
    }
}
