using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

public class CalculateActiveCartShipmentHandler(IActiveCartReads reads, IRequestSender sender)
    : IQueryHandler<CalculateActiveCartShipmentQuery, CalculateShipmentDto[]>
{
    public async Task<CalculateShipmentDto[]> Handle(CalculateActiveCartShipmentQuery req, CancellationToken ct)
    {
        ActiveCartItem[] items = await reads.AllAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false);

        if (!items.Any(x => x.ForDelivery))
            throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: false);

        var weights = await sender.SendQueryAsync(
            new GetCustomizationsWeightByIdsQuery(
                Ids: [..
                    items
                        .Where(i => i.ForDelivery && i.CustomizationId is not null)
                        .Select(i => i.CustomizationId!.Value)
                ]
            ),
            ct
        ).ConfigureAwait(false);

        CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
            new CalculateShipmentQuery(
                ParcelCount: items.Count(x => x.ForDelivery),
                TotalWeight: weights.Sum(x => x.Value),
                Address: req.Address
            ),
            ct
        ).ConfigureAwait(false);

        return calculations;
    }
}
