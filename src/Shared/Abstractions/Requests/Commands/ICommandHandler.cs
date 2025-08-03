namespace CustomCADs.Shared.Abstractions.Requests.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
	Task Handle(TCommand req, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
	Task<TResponse> Handle(TCommand req, CancellationToken ct = default);
}
