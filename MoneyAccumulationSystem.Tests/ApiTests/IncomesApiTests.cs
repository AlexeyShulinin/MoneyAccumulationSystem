using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.WebApi.ApiModels;
using Moq;
using Xunit;

namespace MoneyAccumulationSystem.Tests.ApiTests;

public class IncomesApiTests : BaseApiTest
{
    public IncomesApiTests(MasApplicationFactory applicationFactory) : base(applicationFactory)
    {
        mockIncomeRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Income>()
            {
                new() { Id = 1, UserId = 1, IncomeTypeId = 1, Amount = 800, DateTimeOffset = new DateTimeOffset() },
                new() { Id = 2, UserId = 1, IncomeTypeId = 1, Amount = 900, DateTimeOffset = new DateTimeOffset() }
            });
        
        mockUnitOfWork.Setup(x => x.IncomeRepository)
            .Returns(mockIncomeRepository.Object);
    }

    [Fact]
    public async Task GetListIncomesShouldReturnCorrectValues()
    {
        /*var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJBdXRoVXNlciI6IntcIklkXCI6MX0iLCJuYmYiOjE2NzkwMzU0MzMsImV4cCI6MTY3OTA1NzAzMywiaWF0IjoxNjc5MDM1NDMzLCJpc3MiOiJtYXMuY29tIiwiYXVkIjoibWFzLmNvbSJ9.hHMvfjUaVkMT0h41mae4nDp6b4joNO7xgimzASdyQUVgWfXh2Pnopjf6jdfCZWqo_4ZbXOOH7Lp0WL_Pz31MSA";
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);*/
        
        MockUserClaim(new AuthUser
        {
            Id = 1
        });
        
        var response = await Client.GetFromJsonAsync<IList<IncomeApiModel>>("api/v1.0/Incomes");
        response.Count.Should().BeGreaterThan(0);
    }
}