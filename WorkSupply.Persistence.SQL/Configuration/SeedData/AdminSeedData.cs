using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Persistence.SQL.Configuration.SeedData
{
    public class AdminSeedData : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        // TODO: move to config file
        public static readonly string AdminId = "0";
        public static readonly string AdminEmail = "admin@worksupply.com";
        public static readonly string AdminPassword = "password";

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(new ApplicationUser
            {
                Id = AdminId,
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                PasswordHash = _passwordHasher.HashPassword(null, AdminPassword)
            });
        }
    }
}