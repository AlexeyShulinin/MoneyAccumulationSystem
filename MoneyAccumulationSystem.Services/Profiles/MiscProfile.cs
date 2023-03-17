using AutoMapper;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Services.DtoModels;

namespace MoneyAccumulationSystem.Services.Profiles;

public class MiscProfile : Profile
{
    public MiscProfile()
    {
        CreateMap<IncomeType, ReferenceDtoModel>();
        CreateMap<User, ReferenceDtoModel>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Login));
    }
}