namespace MoneyAccumulationSystem.Database.EF.Models;

public class PaymentSchedule : BaseEntity
{
    public int UserId { get; set; }    
    
    public string Name { get; set; }
    public decimal Amount { get; set; }

    public User User { get; set; }
}