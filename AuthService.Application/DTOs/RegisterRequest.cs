using AuthService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? FullName { get; set; }
        public UserRoles Role { get; set; } = UserRoles.User;
        public string? Phone { get; set; }
        public string? StoreName { get; set; }
    }
}
