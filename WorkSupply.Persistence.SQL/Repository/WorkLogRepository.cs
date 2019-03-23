using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Query;
using WorkSupply.Core.Repository;
using WorkSupply.Persistence.SQL.Data;
using WorkSupply.Persistence.SQL.QueryableExtensions;

namespace WorkSupply.Persistence.SQL.Repository
{
    public class WorkLogRepository : IWorkLogRepository
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<WorkLogRepository>();

        private readonly WorkSupplyContext _context;
        
        public WorkLogRepository(WorkSupplyContext context)
        {
            _context = context;
        }

        public async Task<WorkLog> GetWorkLog(string workLogId)
        {
            var workLog = await _context.WorkLogs
                .Include(wl => wl.Employee)
                .Include(wl => wl.Employer)
                .SingleOrDefaultAsync(wl => wl.Id == workLogId);
            
            if (workLog != null) return workLog;
            
            Log.Warning("Trying to get non existing work log: Id-{workLogId}", workLogId);
            throw new EntityNotFoundException("Could not find work log!");
        }

        public async Task<PaginatedList<WorkLog>> GetWorkLogs(WorkLogQuery filters, string userId)
        {
            var workLogs = _context.WorkLogs
                .Where(wl => (wl.EmployeeId == userId || wl.EmployerId == userId))
                .Include(wl => wl.Employee)
                .Include(wl => wl.Employer)
                .AsQueryable();

            if (filters.StartDate.HasValue)
                workLogs = workLogs.Where(wl => wl.CreatedDate > filters.StartDate);

            if (filters.EndDate.HasValue)
                workLogs = workLogs.Where(wl => wl.CreatedDate < filters.EndDate);

            if (!string.IsNullOrEmpty(filters.FilterId))
                workLogs = workLogs.Where(
                    wl => (wl.EmployeeId == filters.FilterId || wl.EmployerId == filters.FilterId));

            if (filters.Status.HasValue)
                workLogs = workLogs.Where(wl => wl.Status == filters.Status);

            workLogs = workLogs.AsNoTracking();

            return PaginatedList<WorkLog>.Create(workLogs, filters.Page ?? 1, 20);
        }

        public async Task<List<WorkLogGraphData>> GetWorkLogGraphData(WorkLogGraphDataQuery filters, string userId)
        {
            var workLogs = _context.WorkLogs
                .Where(wl => (wl.EmployeeId == userId || wl.EmployerId == userId))
                .AsQueryable();
            
            // Filter by date
            if (filters.StartDate.HasValue)
                workLogs = workLogs.Where(wl => wl.CreatedDate > filters.StartDate);

            if (filters.EndDate.HasValue)
                workLogs = workLogs.Where(wl => wl.CreatedDate < filters.EndDate);

            return await workLogs
                .GroupWorkLogsByGranularity(filters.Granularity)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task AddWorkLog(WorkLog workLog)
        {   
            workLog.CreatedDate = DateTime.Now;
            workLog.Status = WorkLogStatus.PendingApproval;
            
            await _context.WorkLogs.AddAsync(workLog);
        }

        public async Task SetWorkLogInStatus(WorkLog workLog, WorkLogStatus workLogStatus)
        {
            workLog.ModifiedDate = DateTime.Now;
            workLog.Status = workLogStatus;
        }
    }
}