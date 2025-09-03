using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;

public class GetAllShipmentsHandler(IShipmentReads reads, IRequestSender sender)
	: IQueryHandler<GetAllShipmentsQuery, Result<GetAllShipmentsDto>>
{
	public async Task<Result<GetAllShipmentsDto>> Handle(GetAllShipmentsQuery req, CancellationToken ct)
	{
		Result<Shipment> result = await reads.AllAsync(
			query: new(
				CustomerId: req.CustomerId,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		Dictionary<AccountId, string> buyers = await sender.SendQueryAsync(
			new GetUsernamesByIdsQuery([.. result.Items.Select(i => i.BuyerId)]),
			ct
		).ConfigureAwait(false);

		return new(
			Count: result.Count,
			Items: [.. result.Items.Select(i => i.ToGetAllDto(buyers[i.BuyerId]))]
		);
	}
}
