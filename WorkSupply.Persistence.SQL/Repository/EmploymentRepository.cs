using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Employments;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Repository;
using WorkSupply.Persistence.SQL.Data;

namespace WorkSupply.Persistence.SQL.Repository
{
    public class EmploymentRepository : IEmploymentRepository
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<EmploymentRepository>();

        private readonly WorkSupplyContext _context;
        
        public EmploymentRepository(WorkSupplyContext context)
        {
            _context = context;
        }

        public async Task AddEmployment(string employeeId, string employerId)
        {
            // Find if there was already employment 
            var employment =
                await _context.Employments.SingleOrDefaultAsync(e =>
                    e.EmployeeId == employeeId && e.EmployerId == employerId);
            
            // If there was not create new one
            if (employment == null)
            {
                await _context.Employments.AddAsync(new Employment
                {
                    EmployeeId = employeeId,
                    EmployerId = employerId,
                    IsActive = true
                });
                
                return;
            }
            
            // If there was simply set it to active again
            employment.IsActive = true;
        }

        public async Task BreakEmployment(string employeeId, string employerId)
        {
            // find employment with users
            var employment =
                await _context.Employments.SingleOrDefaultAsync(e =>
                    e.EmployeeId == employeeId && e.EmployerId == employerId);
            if (employment == null)
            {
                Log.Warning("Trying to remove employment that does not exist: employer-{employerId} and employee-{employeeId}",
                    employerId, employeeId);
                throw new EntityNotFoundException("There is no employment info with those users");
            }

            // deactivate employment
            employment.IsActive = false;
        }
    }
}