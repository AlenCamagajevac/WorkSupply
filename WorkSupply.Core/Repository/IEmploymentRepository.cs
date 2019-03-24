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
    }
}