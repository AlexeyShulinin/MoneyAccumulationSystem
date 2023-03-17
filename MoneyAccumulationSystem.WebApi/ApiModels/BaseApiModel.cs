using MoneyAccumulationSystem.CrossCutting.Interfaces;

namespace MoneyAccumulationSystem.WebApi.ApiModels;

public class BaseApiModel : IBaseEntity
{
    public int Id { get; set; }
}