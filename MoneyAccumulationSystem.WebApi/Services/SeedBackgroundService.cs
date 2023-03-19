using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;

namespace MoneyAccumulationSystem.WebApi.Services;

public class SeedBackgroundService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    public SeedBackgroundService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<MasDbContext>();
        
        SeedUsers(dbContext);

        SeedIncomeTypes(dbContext);
        SeedIncomes(dbContext);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void SeedUsers(MasDbContext dbContext)
    {
        dbContext.Users.AddRange(new List<User>
        {
            new() { Id = 1, Login = "Alexey", HashedPassword = "17F80754644D33AC685B0842A402229ADBB43FC9312F7BDF36BA24237A1F1FFB" },
            new() { Id = 2, Login = "Test 2", HashedPassword = "17F80754644D33AC685B0842A402229ADBB43FC9312F7BDF36BA24237A1F1FFB" },
        });
    }

    private void SeedIncomeTypes(MasDbContext dbContext)
    {
        dbContext.IncomeTypes.AddRange(new List<IncomeType>
        {
            new() { Id = IncomeType.Contract, Name = nameof(IncomeType.Contract) }
        });
    }

    private void SeedIncomes(MasDbContext dbContext)
    {
        dbContext.Incomes.AddRange(new List<Income>
        {
            new()
            {
                Id = 1, UserId = 1, IncomeTypeId = 1, Amount = 800, 
                DateTimeOffset = new DateTimeOffset(new DateTime(2022, 1, 5), TimeSpan.Zero)
            },
            new()
            {
                Id = 2, UserId = 1, IncomeTypeId = 1, Amount = 900, 
                DateTimeOffset = new DateTimeOffset(new DateTime(2022, 1, 20), TimeSpan.Zero)
            },
            new()
            {
                Id = 3, UserId = 1, IncomeTypeId = 1, Amount = 1000, 
                DateTimeOffset = new DateTimeOffset(new DateTime(2022, 2, 20), TimeSpan.Zero)
            }
        });
    }
}