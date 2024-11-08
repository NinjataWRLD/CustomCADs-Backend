using MediatR;

namespace CustomCADs.Shared.Application.Requests.Queries;

public interface IQuery : IRequest;
public interface IQuery<out TResponse> : IRequest<TResponse>;
