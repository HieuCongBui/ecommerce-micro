using AuthService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.IServices
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken(JwtUser jwtUser);
        public string GenerateRefreshToken();

    }
}
