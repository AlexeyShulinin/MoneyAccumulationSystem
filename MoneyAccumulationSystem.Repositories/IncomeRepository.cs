﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Repositories.Interfaces;

namespace MoneyAccumulationSystem.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly MasDbContext dbContext;
    private readonly AuthUser authUser;
    public IncomeRepository(MasDbContext dbContext, AuthUser authUser)
    {
        this.dbContext = dbContext;
        this.authUser = authUser;
    }
    
    public async Task<IList<Income>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Incomes
            .Where(x => x.UserId == authUser.Id)
            .Include(x => x.User)
            .Include(x => x.IncomeType)
            .ToListAsync(cancellationToken);
    }

    public Task<Income> GetAsync(int incomeId, CancellationToken cancellationToken)
    {
        return dbContext.Incomes
            .Where(x => x.UserId == authUser.Id && x.Id == incomeId)
            .Include(x => x.User)
            .Include(x => x.IncomeType)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Create(Income income, CancellationToken cancellationToken)
    {
        dbContext.Incomes.Add(income);
    }

    public void Update(Income income, CancellationToken cancellationToken)
    {
        dbContext.Update(income);
    }
}