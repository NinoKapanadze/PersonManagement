using Microsoft.Extensions.Logging;
using PersonManagement.Application.RepoInterfaces;
using PersonManagement.Persistance.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PersonManagement.Persistance.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dbContext;
        // private readonly ILogger<WriteRepository<Transaction>> _transactionLogger;

        //  public IUserWriteRepository UserWriteRepository { get; private set; }
        //  public ITransactionWriteRepository TransactionWriteRepository { get; private set; }

        public UnitOfWork()
        {
            
        }

        public IPersonWriteRepository PersonWriteRepository => throw new NotImplementedException();

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
