using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public string? StoreName { get; set; } // For Seller
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
