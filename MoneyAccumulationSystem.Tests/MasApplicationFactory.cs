using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Repositories.Interfaces;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.WebApi;
using Moq;

namespace MoneyAccumulationSystem.Tests;

public class MasApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IIncomeRepository> MockIncomeRepository { get; } = new();
    public Mock<IUnitOfWork> MockUnitOfWork { get; } = new();
    
    public Mock<IHttpContextAccessor> MockHttpContextAccessor { get; } = new();

    public ServiceProvider ServiceProvider;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove<MasDbContext>();
            services.RemoveAll<IHostedService>();

            services.Replace<IHttpContextAccessor>(MockHttpContextAccessor.Object);

            services.Replace<IIncomeRepository>(MockIncomeRepository.Object);
            services.Replace<IUnitOfWork>(MockUnitOfWork.Object);

            ServiceProvider = services.BuildServiceProvider();
        });
        base.ConfigureWebHost(builder);
    }
}