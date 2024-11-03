using MediatR;

namespace CustomCADs.Catalog.Application.Common.Contracts;

public interface IQueryHandler<in TRequest> : IRequestHandler<TRequest>
    where TRequest : IQuery;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>;
