using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

public sealed class GetActiveCartItemsHandler(IActiveCartReads reads, IRequestSender sender)
	: IQueryHandler<GetActiveCartItemsQuery, ActiveCartItemDto[]>
{
	public async Task<ActiveCartItemDto[]> Handle(GetActiveCartItemsQuery req, CancellationToken ct)
	{
		ActiveCartItem[] items = await reads.AllAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);

		string buyer = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(req.BuyerId),
			ct
		).ConfigureAwait(false);

		return [.. items.Select(x => x.ToDto(buyer))];
	}
}
