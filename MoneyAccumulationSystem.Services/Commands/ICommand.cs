using MediatR;

namespace MoneyAccumulationSystem.Services.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}