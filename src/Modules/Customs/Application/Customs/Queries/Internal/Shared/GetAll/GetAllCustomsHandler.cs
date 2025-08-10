using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public sealed class GetAllCustomsHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<GetAllCustomsQuery, Result<GetAllCustomsDto>>
{
	public async Task<Result<GetAllCustomsDto>> Handle(GetAllCustomsQuery req, CancellationToken ct)
	{
		CustomQuery query = new(
			ForDelivery: req.ForDelivery,
			CustomStatus: req.CustomStatus,
			BuyerId: req.BuyerId,
			DesignerId: req.DesignerId,
			Name: req.Name,
			Sorting: req.Sorting,
			Pagination: req.Pagination
		);
		Result<Custom> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

		AccountId[] buyerIds = [.. result.Items.Select(o => o.BuyerId)];
		AccountId[] designerIds = [.. result.Items
			.Where(o => o.AcceptedCustom is not null)
			.Select(o => o.AcceptedCustom!.DesignerId)
		];

		Dictionary<AccountId, string> designers = await sender.SendQueryAsync(
			new GetUsernamesByIdsQuery(designerIds),
			ct
		).ConfigureAwait(false);

		Dictionary<AccountId, string> buyers = await sender.SendQueryAsync(
			new GetUsernamesByIdsQuery(buyerIds),
			ct
		).ConfigureAwait(false);

		return new(
			Count: result.Count,
			Items: [.. result.Items.Select(o => o.ToGetAllDto(
				buyerName: buyers[o.BuyerId],
				designerName: o.AcceptedCustom is null ? null : designers[o.AcceptedCustom.DesignerId]
			))]
		);
	}
}
