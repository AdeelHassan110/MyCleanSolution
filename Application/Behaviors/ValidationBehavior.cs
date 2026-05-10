using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    // ✅ MediatR v8 ke liye sahi parameter order
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        // Agar koi validator exist karta hai is request ke liye
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            // Saare validators ko run karo
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            // Saari errors collect karo
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // Agar koi error hai toh exception throw karo
            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        // Agar validation pass ho gayi toh next handler ko call karo
        return await next();
    }
}