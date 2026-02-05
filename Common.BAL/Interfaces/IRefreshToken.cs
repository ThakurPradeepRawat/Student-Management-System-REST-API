using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Interfaces
{
    public interface IRefreshToken
    {
        string GenerateRefreshToken();
        void InsertRefreshToken(string Email , string refreshToken);
        bool RefreshTokenValidation(string RefreshToken);
        void UpdateToken(string RefreshToken);
    }
}
