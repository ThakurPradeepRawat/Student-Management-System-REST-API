using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Model.DTO
{
    public class LoginResponseDTO
    {
        public string Token {  get; set; }
        public DateTime Expiration { get; set; }

    }
}
