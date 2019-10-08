using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Persistence;
using WorkSupply.Core.Query;
using WorkSupply.Core.Service;

namespace WorkSupply.Services.Services
{
    public class WorkLogService : IWorkLogService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<WorkLogService>();
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public WorkLogService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task SaveWorkLog(WorkLog workLog)
        {
            // Check if employment exists between two users
            if (!await _unitOfWork.Employments.EmploymentExists(workLog.EmployerId, workLog.EmployeeId))
            {
                Log.Warning("Trying to log work for users that have no employment relationship." +
                            " employer:{}, employee:{}", workLog.EmployerId, workLog.EmployeeId);
                throw new EntityNotFoundException("Could not find employment info with those two users");
            }

            await _unitOfWork.WorkLogs.AddWorkLog(workLog);
            await _unitOfWork.CompleteAsync();
        }

        public async Task ResolveWorkLog(string employerId, string workLogId, WorkLogStatus status)
        {
            // Get work log
            var workLog = await GetWorkLogAsync(employerId, workLogId);
            if (workLog.ModifiedDate.HasValue)
            {
                Log.Warning("Trying to update status for already resolved work log: {@workLog}", workLog);
                throw new EntityAlreadyModifiedException("Work log was already approved");
            }
            
            // Set approve status
            await _unitOfWork.WorkLogs.SetWorkLogInStatus(workLog, status);
            
            await _unitOfWork.CompleteAsync();
        }

        public async Task<WorkLog> GetWorkLogAsync(string accessUserId, string workLogId)
        {
            // Find work log - only users who are in work log can get it
            var workLog = await _unitOfWork.WorkLogs.GetWorkLog(workLogId);
            
            if (workLog.EmployerId == accessUserId || workLog.EmployeeId == accessUserId) return workLog;
            
            Log.Warning("User: {@accessUserId} tried to access work log: {@workLog} without access rights",
                accessUserId, workLog);
            throw new PermissionDeniedException("Can't edit that work log!");

        }

        public async Task<PaginatedList<WorkLog>> GetWorkLogs(WorkLogQuery filters, string userId)
        {
            return await _unitOfWork.WorkLogs.GetWorkLogs(filters, userId);
        }

        public async Task<List<WorkLogGraphData>> GetWorkLogGraphData(WorkLogGraphDataQuery filters, string userId)
        {
            return await _unitOfWork.WorkLogs.GetWorkLogGraphData(filters, userId);
        }
    }
}