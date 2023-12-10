using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MoneyAccumulationSystem.CrossCutting.Constants;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Features.Incomes;
using MoneyAccumulationSystem.Services.Features.Reports;
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
    [ProducesResponseType(typeof(BaseListApiModel<IncomeApiModel>), StatusCodes.Status200OK)]
    public async Task<BaseListApiModel<IncomeApiModel>> GetListAsync(CancellationToken cancellationToken)
        => new(mapper.Map<List<IncomeApiModel>>(await mediator.Send(new GetIncomeListQuery(), cancellationToken)));

    /// <summary>
    /// Get Income by Id
    /// </summary>
    /// <param name="incomeId">Income Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Income</returns>
    [HttpGet("{incomeId}")]
    [ProducesResponseType(typeof(IncomeApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IncomeApiModel> GetAsync(int incomeId, CancellationToken cancellationToken)
        => mapper.Map<IncomeApiModel>(await mediator.Send(new GetIncomeQuery { Id = incomeId }, cancellationToken));

    /// <summary>
    /// Create Income
    /// </summary>
    /// <param name="income">Data</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Income</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IncomeApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(IncomeApiModel income, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateIncomeCommand
        {
            Income = mapper.Map<IncomeDtoModel>(income)
        }, cancellationToken);
        return CreatedAtAction("Get", new { incomeId = result }, result);
    }

    /// <summary>
    /// Update Income
    /// </summary>
    /// <param name="incomeId">Income Id</param>
    /// <param name="income">Data</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Income</returns>
    [HttpPut("{incomeId}")]
    [ProducesResponseType(typeof(IncomeApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    public async Task<IncomeApiModel> UpdateAsync(int incomeId, IncomeApiModel income, CancellationToken cancellationToken)
        => mapper.Map<IncomeApiModel>(
            await mediator.Send(
                new UpdateIncomeCommand
                {
                    IncomeId = incomeId,
                    Income = mapper.Map<IncomeDtoModel>(income)
                }, 
                cancellationToken));
    
    /// <summary>
    /// Delete Income
    /// </summary>
    /// <param name="incomeId">Income Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Income</returns>
    [HttpDelete("{incomeId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(int incomeId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteIncomeCommand { IncomeId = incomeId }, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Generate excel report
    /// </summary>
    /// <param name="year">Year of report</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{year}/report")]
    public async Task<FileResult> GetYearlyReportAsync(int year, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GenerateYearlyReportQuery { Year = year }, cancellationToken);

        return File(result, ContentTypes.Excel, $"Yearly_Income_Report_Per-{year}.xlsx");
    }
}