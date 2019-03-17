using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSupply.Core.Models.AppUser;

namespace WorkSupply.Persistence.SQL.Configuration.SeedData
{
    public class RolesSeedData : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            var adminName = 
            
            builder.HasData(new ApplicationRole
            {
                Id = ((int)Role.Admin).ToString(),
                Name = ApplicationRole.GetRoleName(Role.Admin),
                NormalizedName = ApplicationRole.GetRoleName(Role.Admin).ToUpper(),
                Description = "Admin of WorkSupply app"
            }, new ApplicationRole 
            {
                
                Id = ((int)Role.Employee).ToString(),
                Name = ApplicationRole.GetRoleName(Role.Employee),
                NormalizedName = ApplicationRole.GetRoleName(Role.Employee).ToUpper()
            }, new ApplicationRole 
            {
                Id = ((int)Role.Employer).ToString(),
                Name = ApplicationRole.GetRoleName(Role.Employer),
                NormalizedName = ApplicationRole.GetRoleName(Role.Employer).ToUpper()
            });
        }
    }
}