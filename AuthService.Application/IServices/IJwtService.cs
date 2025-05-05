using AuthService.Application.DTOs;

namespace AuthService.Application.IServices
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken(JwtUser jwtUser);
        public string GenerateRefreshToken();
    }
}
