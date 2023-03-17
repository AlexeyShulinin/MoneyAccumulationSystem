using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyAccumulationSystem.Services.Features.Incomes;
using MoneyAccumulationSystem.WebApi.ApiModels;

namespace MoneyAccumulationSystem.WebApi.Controllers;

[ApiVersion("1.0")]
public class IncomesController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public IncomesController (IMediator mediator, IMapper mapper) 
    { 
        this.mediator = mediator;
        this.mapper = mapper;
    }
    
    /// <summary>
    /// Get list of user's Incomes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of user's Incomes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<IncomeApiModel>), StatusCodes.Status200OK)]
    public async Task<IList<IncomeApiModel>> GetList(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetIncomeListQuery(), cancellationToken);
        return mapper.Map<List<IncomeApiModel>>(result);
    }
}