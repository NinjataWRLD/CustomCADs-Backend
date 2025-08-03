namespace CustomCADs.Shared.Abstractions.Requests.Queries;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
	Task<TResponse> Handle(TQuery req, CancellationToken ct = default);
}
