using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Persistence;
using WorkSupply.Core.Service;

namespace WorkSupply.Services.Services
{
    public class EmploymentService : IEmploymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public EmploymentService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task SetEmploymentStatus(string employeeId, string employerId, bool relationshipShouldExist)
        {
            // Find employee, find employer
            var employee = await _userManager.FindByIdAsync(employeeId);
            if (employee == null)
            {
                // TODO: log
                throw new EntityNotFoundException("Could not find employee");
            }

            var employer = await _userManager.FindByIdAsync(employerId);
            if (employer == null)
            {
                // TODO: log
                throw new EntityNotFoundException("Could not find employer");
            }

            // Set employment status
            if (relationshipShouldExist) await _unitOfWork.Employments.AddEmployment(employee.Id, employer.Id);
            else await _unitOfWork.Employments.BreakEmployment(employee.Id, employer.Id);
            await _unitOfWork.CompleteAsync();
        }
    }
}