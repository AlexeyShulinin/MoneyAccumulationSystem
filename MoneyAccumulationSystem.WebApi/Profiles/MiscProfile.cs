using AutoMapper;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.WebApi.ApiModels;

namespace MoneyAccumulationSystem.WebApi.Profiles;

public class MiscProfile : Profile
{
    public MiscProfile()
    {
        CreateMap<ReferenceDtoModel, ReferenceApiModel>();
        CreateMap<ReferenceApiModel, ReferenceDtoModel>();
        CreateMap<CurrencyReferenceDtoModel, CurrencyReferenceApiModel>();
    }
}