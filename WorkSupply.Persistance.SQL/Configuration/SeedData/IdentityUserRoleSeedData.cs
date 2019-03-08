﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Persistance.SQL.Configuration.SeedData
{
    public class IdentityUserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });
        }
    }
}