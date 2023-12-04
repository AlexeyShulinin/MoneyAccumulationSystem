namespace MoneyAccumulationSystem.Database.EF.Models;

public class SchedulePeriod : BaseEntity
{
    public string Name { get; set; }

    public const int Daily = 1;
    public const int Weekly = 2;
    public const int Monthly = 3;
}