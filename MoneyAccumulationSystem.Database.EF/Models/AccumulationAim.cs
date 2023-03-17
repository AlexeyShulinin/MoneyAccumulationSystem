using System;

namespace MoneyAccumulationSystem.Database.EF.Models;

public class AccumulationAim : BaseEntity
{
    public decimal Amount { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Notes { get; set; }

    public bool IsAchieved { get; set; }
}