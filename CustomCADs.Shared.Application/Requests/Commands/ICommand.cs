using MediatR;

namespace CustomCADs.Shared.Application.Requests.Commands;

public interface ICommand : IRequest;
public interface ICommand<out TResponse> : IRequest<TResponse>;
