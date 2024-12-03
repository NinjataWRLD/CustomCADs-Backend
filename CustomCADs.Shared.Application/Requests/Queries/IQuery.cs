using MediatR;

namespace CustomCADs.Shared.Application.Requests.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>;
