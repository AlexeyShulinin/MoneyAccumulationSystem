namespace MoneyAccumulationSystem.Services.DtoModels;

public class UserDtoModel : BaseDtoModel
{
    public string Login { get; set; }
    public string HashedPassword { get; set; }
}