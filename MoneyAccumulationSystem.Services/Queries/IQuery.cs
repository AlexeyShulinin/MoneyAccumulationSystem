using MediatR;

namespace MoneyAccumulationSystem.Services.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}