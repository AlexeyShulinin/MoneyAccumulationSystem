﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.Repositories.Interfaces;

public interface IIncomeRepository
{
    Task<IList<Income>> GetListAsync(CancellationToken cancellationToken);
    Task<Income> GetAsync(int incomeId, CancellationToken cancellationToken);
    void Create(Income income);
    void Update(Income income);
    void Delete(Income income);
    Task<IDictionary<DateTime, decimal>> GetYearlyDictionaryAsync(int year, CancellationToken cancellationToken);
}