using System.Threading.Tasks;

namespace WorkSupply.Core.Service
{
    public interface IEmploymentService
    {
        /// <summary>
        /// Changes the status of relationship between employer and employee.
        /// Creates Employment record if necessary
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employerId"></param>
        /// <param name="relationshipShouldExist"></param>
        /// <returns></returns>
        Task SetEmploymentStatus(string employeeId, string employerId, bool relationshipShouldExist);
    }
}