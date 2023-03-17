namespace MoneyAccumulationSystem.Database.EF.Models;

public class IncomeType : BaseEntity
{
    public string Name { get; set; }

    public const int Contract = 1;
}