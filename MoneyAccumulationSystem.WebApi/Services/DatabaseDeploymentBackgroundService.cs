using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyAccumulationSystem.Database.EF;

namespace MoneyAccumulationSystem.WebApi.Services;

public class DatabaseDeploymentBackgroundService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    public DatabaseDeploymentBackgroundService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<MasDbContext>();

        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }
}