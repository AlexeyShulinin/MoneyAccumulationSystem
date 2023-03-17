using MoneyAccumulationSystem.CrossCutting.Interfaces;

namespace MoneyAccumulationSystem.Services.DtoModels;

public class BaseDtoModel : IBaseEntity
{
    public int Id { get; set; }
}