using AuthService.Application;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure
{
    public class AuthDbContext :DbContext, IDataContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //  Account and UserProfile: 1-1
            modelBuilder.Entity<UserProfile>()
                .HasOne(u => u.Account) 
                .WithOne(a => a.Profile) 
                .HasForeignKey<UserProfile>(u => u.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Account and RefreshToken: 1-n
            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.Account) 
                .WithMany(a => a.RefreshTokens) 
                .HasForeignKey(r => r.AccountId) 
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
