using System;

namespace MoneyAccumulationSystem.Database.EF.Models;

public class Expenditure : BaseEntity
{
    public int UserId { get; set; }
    
    public decimal Amount { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public string Notes { get; set; }

    public int ExpenditureTypeId { get; set; }

    public User User { get; set; }
    public ExpenditureType ExpenditureType { get; set; }
}