using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Persistance.SQL.Configuration.SeedData
{
    public class AdminSeedData : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();
        
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(new ApplicationUser
            {
                Id = "1",
                UserName = "WorkSupply Admin",
                NormalizedUserName = "WORKSUPPLY ADMIN",
                Email = "admin@worksupply.com",
                EmailConfirmed = true,
                PasswordHash = _passwordHasher.HashPassword(null, "password")
            });
        }
    }
}