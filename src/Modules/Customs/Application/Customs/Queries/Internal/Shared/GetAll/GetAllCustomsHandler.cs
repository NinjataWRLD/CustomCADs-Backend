using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

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

        GetUsernamesByIdsQuery designerUsernamesQuery = new(designerIds);
        Dictionary<AccountId, string> designers = await sender
            .SendQueryAsync(designerUsernamesQuery, ct).ConfigureAwait(false);

        GetUsernamesByIdsQuery buyerUsernamesQuery = new(buyerIds);
        Dictionary<AccountId, string> buyers = await sender
            .SendQueryAsync(buyerUsernamesQuery, ct).ConfigureAwait(false);

        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        Dictionary<AccountId, string> timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: [.. result.Items.Select(o => o.ToGetAllDto(
                buyerName: buyers[o.BuyerId],
                designerName: o.AcceptedCustom is null ? null : designers[o.AcceptedCustom.DesignerId],
                timeZone: timeZones[o.BuyerId]
            ))]
        );
    }
}
