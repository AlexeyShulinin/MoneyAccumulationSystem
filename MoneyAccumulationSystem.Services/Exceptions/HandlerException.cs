using System;

namespace MoneyAccumulationSystem.Services.Exceptions;

public class HandlerException : Exception
{
    public HandlerException() : base() { }
    public HandlerException(string message) : base(message) { }
}