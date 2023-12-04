using System;
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
            .ForMember(x => x.DateTimeOffset, opt => opt.MapFrom(x => x.DateTimeOffset ?? DateTimeOffset.UtcNow))
            .ForMember(x => x.UserId, opt => opt.MapFrom((_, x) => x.UserId))
            .ForMember(x => x.User, opt => opt.UseDestinationValue())
            .ForMember(x => x.IncomeTypeId, opt => opt.MapFrom((x, _) => x.IncomeType?.Id))
            .ForMember(x => x.IncomeType, opt => opt.Ignore());
    }
}