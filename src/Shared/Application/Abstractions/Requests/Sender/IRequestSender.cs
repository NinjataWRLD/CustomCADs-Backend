namespace CustomCADs.Shared.Application.Abstractions.Requests.Sender;

public interface IRequestSender
{
	Task SendCommandAsync(ICommand command, CancellationToken ct = default);
	Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
	Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken ct = default);
}
