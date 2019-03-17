using System.Threading.Tasks;
using WorkSupply.Core.Persistence;
using WorkSupply.Core.Repository;
using WorkSupply.Persistence.SQL.Data;
using WorkSupply.Persistence.SQL.Repository;

namespace WorkSupply.Persistence.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkSupplyContext _context;
        
        public IWorkLogRepository WorkLogs { get; private set; }

        public UnitOfWork(WorkSupplyContext context)
        {
            _context = context;
            WorkLogs = new WorkLogRepository(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}