using System;
using System.Threading.Tasks;
using WorkSupply.Core.Repository;

namespace WorkSupply.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkLogRepository WorkLogs { get; }
        IEmploymentRepository Employments { get; }
        Task<int> CompleteAsync();
    }
}