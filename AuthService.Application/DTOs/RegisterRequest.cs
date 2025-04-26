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
        [Required(ErrorMessage = "User Name is require!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is require!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is require!")]
        [Compare("Password", ErrorMessage = "Confirm Password does not match with Password!")]
        public string ConfirmPassword { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public UserRoles Role { get; set; } = UserRoles.User;
        public string? Phone { get; set; }
        public string? StoreName { get; set; }
    }
}
