using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CustomCADs.Shared.Abstractions.Requests.Middleware;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<IRequest<TResponse>>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest req, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!validators.Any())
        {
            return await next().ConfigureAwait(false);
        }

        ValidationFailure[] errors = [.. validators.SelectMany(r => r.Validate(req).Errors).Distinct()];
        if (errors.Length != 0) throw new ValidationException(errors);

        return await next().ConfigureAwait(false);
    }
}
