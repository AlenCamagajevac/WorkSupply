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
        /// Gets all employees for certain employer
        /// </summary>
        /// <param name="employerId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicationUser>> GetEmployeesForUser(string employerId, int page);

        /// <summary>
        /// Gets all employers for certain employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PaginatedList<ApplicationUser>> GetEmployersForUser(string employeeId, int page);
    }
}