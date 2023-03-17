namespace MoneyAccumulationSystem.Database.EF.Models;

public class ExpenditureType : BaseEntity
{
    public string Name { get; set; }

    public const int Single = 1;
    public const int Monthly = 2;
}