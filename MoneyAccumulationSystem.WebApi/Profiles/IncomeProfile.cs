using AutoMapper;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.WebApi.ApiModels;

namespace MoneyAccumulationSystem.WebApi.Profiles;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<IncomeDtoModel, IncomeApiModel>();
    }
}