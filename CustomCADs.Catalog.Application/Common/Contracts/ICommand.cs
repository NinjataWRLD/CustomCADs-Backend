using MediatR;

namespace CustomCADs.Catalog.Application.Common.Contracts;

public interface ICommand : IRequest;
public interface ICommand<out TResponse> : IRequest<TResponse>;
