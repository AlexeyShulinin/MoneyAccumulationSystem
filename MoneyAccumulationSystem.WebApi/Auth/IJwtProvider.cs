using MoneyAccumulationSystem.CrossCutting.Auth;

namespace MoneyAccumulationSystem.WebApi.Auth;

public interface IJwtProvider
{
    string Generate(AuthUser authUser);
}