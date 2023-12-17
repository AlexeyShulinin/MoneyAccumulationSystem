namespace MoneyAccumulationSystem.Database.EF.Models;

public class Currency : BaseEntity
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    
    public const int Usd = 1;
}