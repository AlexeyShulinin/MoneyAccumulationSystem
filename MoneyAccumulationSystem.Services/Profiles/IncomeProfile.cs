using AutoMapper;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Services.DtoModels;

namespace MoneyAccumulationSystem.Services.Profiles;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<Income, IncomeDtoModel>();
        CreateMap<IncomeDtoModel, Income>()
            .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
            .ForMember(x => x.User, opt => opt.Ignore())
            .ForMember(x => x.IncomeTypeId, opt => opt.MapFrom((x, _) => x.IncomeType?.Id))
            .ForMember(x => x.IncomeType, opt => opt.Ignore());
    }
}