using System.Threading;
using System.Threading.Tasks;
using MoneyAccumulationSystem.Repositories.Interfaces;

namespace MoneyAccumulationSystem.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    #region Repositories
    
    public IIncomeRepository IncomeRepository { get; }
    public IUsersRepository UsersRepository { get; }

    #endregion
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}