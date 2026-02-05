using Common.BAL.Interfaces;
using Common.DAL.Exceptions;
using Common.DAL.Interface;
using Common.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Services
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly IStudentRepository _studentRepository;
        public RefreshTokenService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[64];
            using (var rng  = RandomNumberGenerator.Create())
            {
                rng.GetBytes(RandomNumber); 
            }
            return Convert.ToBase64String(RandomNumber);
        }
        public void InsertRefreshToken(string Email, string refreshToken)
        {
            RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO
                {
                Email = Email,
                RefreshToken = refreshToken,
                ExpiredAt = DateTime.Now.AddDays(1)

          };
            _studentRepository.InsertRefreshToken(refreshTokenDTO);
            
        }

        public bool RefreshTokenValidation(string RefreshToken)
        {
            var RefreshTokenDetail = _studentRepository.GetRefreshTokenDetail(RefreshToken);
            if (RefreshTokenDetail == null)
            {
                return false;
            }
            if ((RefreshToken == RefreshTokenDetail.RefreshToken) && (RefreshTokenDetail.IsRevoked is false ) && (RefreshTokenDetail.ExpiredAt > DateTime.UtcNow)){
                return true;
            }
            return false;
        }
        
        public void UpdateToken(string RefreshToken)
        {
            _studentRepository.UpdateRefreshTokenDetail(RefreshToken); 
        }
    }


}
