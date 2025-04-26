using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="User Name is require!")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is require!")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
