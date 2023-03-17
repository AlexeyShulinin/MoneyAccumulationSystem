using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.CrossCutting.Extensions;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Repositories.Interfaces;

namespace MoneyAccumulationSystem.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MasDbContext dbContext;
    private readonly AuthUser authUser;

    private IncomeRepository incomeRepository;
    private IUsersRepository usersRepository;
    
    public UnitOfWork(MasDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.authUser = httpContextAccessor.GetAuthUserFromClaims();
    }

    public IIncomeRepository IncomeRepository => incomeRepository ??= new IncomeRepository(dbContext, authUser);
    public IUsersRepository UsersRepository => usersRepository ??= new UsersRepository(dbContext, authUser);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}