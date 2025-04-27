using AuthService.Application.DTOs;
using AuthService.Application.Repositories;
using AuthService.Application.Services;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Services
{
    public class UserService:Service<Account>, IUserService
    {
        public UserService(IRepositoryAsync<Account> repository):base(repository)
        {

        }

        public Task<Account> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
