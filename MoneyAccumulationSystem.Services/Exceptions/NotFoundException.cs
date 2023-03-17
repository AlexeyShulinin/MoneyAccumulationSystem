namespace MoneyAccumulationSystem.Services.Exceptions;

public class NotFoundException : HandlerException
{
    private const string NotFoundMessage = "Entity not found";
    public NotFoundException(string message = NotFoundMessage) : base(message) { }
}