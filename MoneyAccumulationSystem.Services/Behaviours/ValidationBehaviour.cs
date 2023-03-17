using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = MoneyAccumulationSystem.Services.Exceptions.ValidationException;

namespace MoneyAccumulationSystem.Services.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> logger;
    private readonly IEnumerable<IValidator<TRequest>> validators;
 
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    {
        this.validators = validators;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            ValidationResult[] validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessage) => new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }
 
        return await next();
    }
}