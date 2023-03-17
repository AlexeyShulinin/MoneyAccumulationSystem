namespace MoneyAccumulationSystem.Database.EF.Models;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string HashedPassword { get; set; }
}