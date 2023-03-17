using AutoMapper;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.WebApi.ApiModels;

namespace MoneyAccumulationSystem.WebApi.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDtoModel, UserApiModel>();
    }
}