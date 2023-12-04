using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoneyAccumulationSystem.CrossCutting.Auth;
using MoneyAccumulationSystem.Services.Features.Users;
using MoneyAccumulationSystem.WebApi.ApiModels;
using MoneyAccumulationSystem.WebApi.Auth;

namespace MoneyAccumulationSystem.WebApi.Controllers;

[ApiVersion("1.0")]
public class UsersController : BaseApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IJwtProvider jwtProvider;
    
    public UsersController (IMediator mediator, IMapper mapper, IConfiguration configuration, IJwtProvider jwtProvider) 
    { 
        this.mediator = mediator;
        this.mapper = mapper;
        this.configuration = configuration;
        this.jwtProvider = jwtProvider;
    }

    /// <summary>
    /// Get user token
    /// </summary>
    /// <param name="userToken">Data</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
    public async Task<IResult> GetUserTokenAsync(UserTokenApiModel userToken, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserQuery
        {
            Login = userToken.Login, 
            Password = userToken.Password
        }, cancellationToken);

        var authUser = new AuthUser
        {
            Id = result.Id
        };

        jwtProvider.Generate(authUser);

        return Results.Ok(jwtProvider.Generate(authUser));
    }

    /// <summary>
    /// Get list of Users
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Users</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<UserApiModel>), StatusCodes.Status200OK)]
    public async Task<IList<UserApiModel>> GetListAsync(CancellationToken cancellationToken)
        => mapper.Map<List<UserApiModel>>(await mediator.Send(new GetUserListQuery(), cancellationToken));
}