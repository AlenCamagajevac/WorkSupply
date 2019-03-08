using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Persistance.SQL.Configuration.SeedData
{
    public class RolesSeedData : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole
            {
                Id = "1",
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "Admin of WorkSupply app"
            }, new ApplicationRole 
            {
                
                Id = "2",
                Name = "employer",
                NormalizedName = "EMPLOYER"
            }, new ApplicationRole 
            {
                Id = "3",
                Name = "employee",
                NormalizedName = "EMPLOYEE"
            });
        }
    }
}