using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetSingle;

public sealed class GetActiveCartItemHandler(IActiveCartReads reads, IRequestSender sender)
	: IQueryHandler<GetActiveCartItemQuery, ActiveCartItemDto>
{
	public async Task<ActiveCartItemDto> Handle(GetActiveCartItemQuery req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.BuyerId, req.ProductId, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.BuyerId, req.ProductId });

		string buyer = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(req.BuyerId),
			ct
		).ConfigureAwait(false);

		return item.ToDto(buyer);
	}
}
