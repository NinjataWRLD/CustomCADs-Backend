using MediatR;

namespace CustomCADs.Account.Application.Common.Contracts;

public interface IQuery : IRequest;
public interface IQuery<out TResponse> : IRequest<TResponse>;
