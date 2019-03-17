using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Persistence.SQL.Configuration.SeedData;

namespace WorkSupply.Persistence.SQL.Data
{
    public class WorkSupplyContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<WorkLog> WorkLogs { get; set; }
        
        public WorkSupplyContext(DbContextOptions options) : base(options)
        {
        
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: Move initial admin and role configs to settings file
            modelBuilder.ApplyConfiguration(new AdminSeedData());
            modelBuilder.ApplyConfiguration(new RolesSeedData());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleSeedData());
        }
    }
}