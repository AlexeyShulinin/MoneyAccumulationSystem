using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.CrossCutting.Extensions;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;
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
        FillUpdatedEntity();
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private void FillUpdatedEntity()
    {
        var userId = authUser?.Id;

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        baseEntity.CreatedDate = DateTimeOffset.Now;
                        baseEntity.CreatedByUserId = userId;
                        break;
                    case EntityState.Modified:
                        baseEntity.UpdatedDate = DateTimeOffset.Now;
                        baseEntity.UpdatedByUserId = userId;
                        break;
                }
            }
        }
    }
}