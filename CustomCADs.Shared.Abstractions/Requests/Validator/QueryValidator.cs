using CustomCADs.Shared.Abstractions.Requests.Queries;
using FluentValidation;

namespace CustomCADs.Shared.Abstractions.Requests.Validator;

public class QueryValidator<TQuery, TResponse> : AbstractValidator<TQuery> where TQuery : IQuery<TResponse>;
