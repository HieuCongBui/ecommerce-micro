using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool isRevoked { get; set; } = false;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    }
}
