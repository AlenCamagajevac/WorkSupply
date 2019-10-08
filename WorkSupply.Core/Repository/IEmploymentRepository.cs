using System.Collections.Generic;
using System.Threading.Tasks;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;

namespace WorkSupply.Core.Repository
{
    public interface IEmploymentRepository
    {
        /// <summary>
        /// Adds new employment between employee and employer
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task AddEmployment(string employeeId, string employerId);

        /// <summary>
        /// Breaks employment between two users
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task BreakEmployment(string employeeId, string employerId);

        /// <summary>
        /// Returns if there is employment info with those two users
        /// </summary>
        /// <param name="employerId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<bool> EmploymentExists(string employerId, string employeeId);

        /// <summary>
        /// Gets list of employees for a given user
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetEmployeesForUser(string employerId);

        /// <summary>
        /// Gets list of employers for a given user
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetEmployersForUser(string employeeId);
    }
}