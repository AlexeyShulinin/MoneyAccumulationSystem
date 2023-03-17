namespace MoneyAccumulationSystem.WebApi.ApiModels;

public class UserApiModel : BaseApiModel
{
    public string Login { get; set; }
    public string HashedPassword { get; set; }
}