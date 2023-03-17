using AutoMapper;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Services.DtoModels;

namespace MoneyAccumulationSystem.Services.Profiles;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<Income, IncomeDtoModel>();
    }
}