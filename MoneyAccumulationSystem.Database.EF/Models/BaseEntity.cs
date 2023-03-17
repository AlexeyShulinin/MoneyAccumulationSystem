using MoneyAccumulationSystem.CrossCutting.Interfaces;

namespace MoneyAccumulationSystem.Database.EF.Models;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}