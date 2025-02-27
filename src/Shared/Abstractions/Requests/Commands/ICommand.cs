using MediatR;

namespace CustomCADs.Shared.Abstractions.Requests.Commands;

public interface ICommand : IRequest;
public interface ICommand<out TResponse> : IRequest<TResponse>;
