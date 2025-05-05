using AuthService.Application.DTOs;
using AuthService.Application.IServices;
using AuthService.Application.Repositories;
using AuthService.Application.Services;
using AuthService.Application.UnitOfWork;
using AuthService.Domain.Entities;
using AuthService.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Services
{
    public class UserService : Service<Account>, IUserService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public UserService(ITokenGenerator tokenGenerator,
            IConfiguration configuration,
            IRepositoryAsync<Account> repository,
            IUnitOfWorkAsync unitOfWork) : base(repository)
        {
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> LoginAsync(LoginRequest request)
        {
            Account user = await _repository.Queryable().FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Wrong password");
            }
            var jwtUser = new JwtUser { Role = user.Role, Email = user.Email, Username = user.Username, UserId = user.Id };
            var access_Token = _tokenGenerator.GenerateJwtToken(jwtUser);
            var reshreshToken = _tokenGenerator.GenerateRefreshToken();

            return user;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var userEmail = await _repository.Queryable().AnyAsync(x => x.Email == request.Email);
                if (userEmail)
                {
                    throw new UnauthorizedAccessException("Email already exists");
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new Account
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = UserRoles.User.ToString(),
                };
                await Task.Run(() => _repository.Insert(user));
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        #region Helper Method
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        #endregion
    }
}
