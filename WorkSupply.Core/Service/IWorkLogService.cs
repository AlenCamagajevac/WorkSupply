using System.Collections.Generic;
using System.Threading.Tasks;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Query;

namespace WorkSupply.Core.Service
{
    public interface IWorkLogService
    {
        /// <summary>
        /// Gets the work log
        /// </summary>
        /// <param name="accessUserId">user trying to access work log</param>
        /// <param name="workLogId">Id of a work log</param>
        /// <returns></returns>
        Task<WorkLog> GetWorkLogAsync(string accessUserId, string workLogId);

        /// <summary>
        /// Gets a work logs for certain user
        /// Returns 20 logs paginated
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="userId">user accessing the work logs</param>
        /// <returns></returns>
        Task<PaginatedList<WorkLog>> GetWorkLogs(WorkLogQuery filters, string userId);
        
        /// <summary>
        /// Persists work log to Db
        /// </summary>
        /// <param name="workLog">Work log to persist</param>
        /// <returns></returns>
        Task SaveWorkLog(WorkLog workLog);

        /// <summary>
        /// Resolves work log to new status 
        /// </summary>
        /// <param name="employerId">employer trying to resolve work log</param>
        /// <param name="workLogId">Id of a work log to resolve</param>
        /// <param name="status">new status of a work log</param>
        /// <returns></returns>
        Task ResolveWorkLog(string employerId, string workLogId, WorkLogStatus status);

        /// <summary>
        /// Gets work log data to display on a chart (grouped by date and sum by total work hours)
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<WorkLogGraphData>> GetWorkLogGraphData(WorkLogGraphDataQuery filters, string userId);
    }
}