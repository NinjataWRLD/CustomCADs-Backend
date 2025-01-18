using MediatR;

namespace CustomCADs.Shared.Abstractions.Requests.Queries;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>;
