using System.Threading.Tasks;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Query;

namespace WorkSupply.Core.Repository
{
    public interface IWorkLogRepository
    {
        /// <summary>
        /// Get a single work log
        /// </summary>
        /// <param name="workLogId">Id of a work log to get</param>
        /// <returns></returns>
        Task<WorkLog> GetWorkLog(string workLogId);

        /// <summary>
        /// Get paginated work logs for a user.
        /// Returns 20 items by default
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PaginatedList<WorkLog>> GetWorkLogs(WorkLogQuery filters, string userId);
        
        /// <summary>
        /// Persists new work log
        /// </summary>
        /// <param name="workLog">work log details</param>
        /// <returns></returns>
        Task AddWorkLog(WorkLog workLog);

        /// <summary>
        /// Moves work log to given status
        /// </summary>
        /// <param name="workLog"></param>
        /// <param name="workLogStatus">new status of a work log</param>
        /// <returns></returns>
        Task SetWorkLogInStatus(WorkLog workLog, WorkLogStatus workLogStatus);
    }
}