using MediatR;

namespace CustomCADs.Account.Application.Common.Contracts;

public interface ICommand : IRequest;
public interface ICommand<out TResponse> : IRequest<TResponse>;
