namespace CustomCADs.Shared.Abstractions.Requests.Commands;

public interface ICommandHandler<in TCommand>
{
	Task Handle(TCommand req, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand, TResponse>
{
	Task<TResponse> Handle(TCommand req, CancellationToken ct = default);
}
