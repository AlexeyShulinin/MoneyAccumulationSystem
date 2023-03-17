using AutoMapper;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Services.DtoModels;

namespace MoneyAccumulationSystem.Services.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDtoModel>();
    }
}