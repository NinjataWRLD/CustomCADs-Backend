using FluentValidation;

namespace CustomCADs.Shared.Application.Abstractions.Requests.Validator;

public class QueryValidator<TQuery, TResponse> : AbstractValidator<TQuery> where TQuery : IQuery<TResponse>;
