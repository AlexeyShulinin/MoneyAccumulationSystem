using System.Collections.Generic;

namespace MoneyAccumulationSystem.Services.Exceptions;

public class ValidationException : HandlerException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}