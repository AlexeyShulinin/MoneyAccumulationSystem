using System;

namespace MoneyAccumulationSystem.Database.EF.Models;

public class Income : BaseEntity
{
    public int UserId { get; set; }
    
    public decimal Amount { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public string Notes { get; set; }
    
    public int CurrencyId { get; set; }
    public int? IncomeTypeId { get; set; }
    
    public User User { get; set; }
    public Currency Currency { get; set; }
    public IncomeType IncomeType { get; set; }
}