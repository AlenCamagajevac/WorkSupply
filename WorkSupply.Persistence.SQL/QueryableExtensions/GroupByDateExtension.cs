using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkSupply.Core.Models.WorkLog;
using WorkSupply.Core.Query;

namespace WorkSupply.Persistence.SQL.QueryableExtensions
{
    public static class GroupByDateExtension
    {
        public static IQueryable<WorkLogGraphData> GroupWorkLogsByGranularity(this IQueryable<WorkLog> workLogs,
            TimeGranularity timeGranularity)
        {
            switch (timeGranularity)
            {
                case TimeGranularity.Day:
                    return workLogs.Select(wl => new
                        {
                            CreatedDay = wl.CreatedDate.Date.Day,
                            CreatedMonth = wl.CreatedDate.Date.Month,
                            CreatedYear = wl.CreatedDate.Date.Year,
                            wl.Duration,
                            wl.Status
                        }).GroupBy(wl => new {wl.CreatedYear, wl.CreatedMonth, wl.CreatedDay})
                        .Select(workLogGroup => new WorkLogGraphData
                        {
                            Date = $"{workLogGroup.Key.CreatedYear:D4}" +
                                   $"-{workLogGroup.Key.CreatedMonth:D2}" +
                                   $"-{workLogGroup.Key.CreatedDay:D2}",
                            TotalHours = workLogGroup.Sum(wl => wl.Duration),
                            TotalApproved =
                                workLogGroup.Sum(wl => wl.Status == WorkLogStatus.Approved ? wl.Duration : 0),
                            TotalRejected =
                                workLogGroup.Sum(wl => wl.Status == WorkLogStatus.Rejected ? wl.Duration : 0),
                            TotalPending = workLogGroup.Sum(wl =>
                                wl.Status == WorkLogStatus.PendingApproval ? wl.Duration : 0)
                        }); 
                
                case TimeGranularity.Month:
                    return workLogs.Select(wl => new
                        {
                            CreatedMonth = wl.CreatedDate.Date.Month,
                            CreatedYear = wl.CreatedDate.Date.Year,
                            wl.Duration,
                            wl.Status
                        }).GroupBy(wl => new {wl.CreatedYear, wl.CreatedMonth})
                        .Select(workLogGroup => new WorkLogGraphData
                        {
                            Date = $"{workLogGroup.Key.CreatedYear:D4}" +
                                   $"-{workLogGroup.Key.CreatedMonth:D2}",
                            TotalHours = workLogGroup.Sum(wl => wl.Duration),
                            TotalApproved =
                                workLogGroup.Sum(wl => wl.Status == WorkLogStatus.Approved ? wl.Duration : 0),
                            TotalRejected =
                                workLogGroup.Sum(wl => wl.Status == WorkLogStatus.Rejected ? wl.Duration : 0),
                            TotalPending = workLogGroup.Sum(wl =>
                                wl.Status == WorkLogStatus.PendingApproval ? wl.Duration : 0)
                        });
                
                default:
                    throw new ArgumentException("Wrong time granularity specified!");
            }
        }
    }
}