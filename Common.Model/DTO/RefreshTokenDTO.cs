using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.DTO
{
    public class RefreshTokenDTO
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
