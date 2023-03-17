using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.Repositories.Interfaces;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.WebApi.Auth;
using Moq;
using Xunit;

namespace MoneyAccumulationSystem.Tests.ApiTests;

public class BaseApiTest : IClassFixture<MasApplicationFactory>
{
    protected readonly HttpClient Client;
    protected readonly MasApplicationFactory Application;

    protected readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;
    protected readonly Mock<IIncomeRepository> mockIncomeRepository;
    protected readonly Mock<IUnitOfWork> mockUnitOfWork;

    protected readonly IJwtProvider jwtProvider;

    public BaseApiTest(MasApplicationFactory application)
    {
        Application = application;
        Client = application.CreateClient();

        mockIncomeRepository = Application.MockIncomeRepository;
        mockHttpContextAccessor = Application.MockHttpContextAccessor;
        mockUnitOfWork = Application.MockUnitOfWork;
        
        jwtProvider = Application.ServiceProvider.GetService<IJwtProvider>();
    }

    public void MockUserClaim(AuthUser authUser)
    {
        var token = jwtProvider.Generate(authUser);
        
        Client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }
}