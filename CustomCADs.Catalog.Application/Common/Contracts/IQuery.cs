using MediatR;

namespace CustomCADs.Catalog.Application.Common.Contracts;

public interface IQuery : IRequest;
public interface IQuery<out TResponse> : IRequest<TResponse>;
