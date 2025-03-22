using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public class CalculateActiveCartShipmentHandler(IActiveCartReads reads, IRequestSender sender)
    : IQueryHandler<CalculateActiveCartShipmentQuery, CalculateActiveCartShipmentDto[]>
{
    public async Task<CalculateActiveCartShipmentDto[]> Handle(CalculateActiveCartShipmentQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        if (!cart.HasDelivery)
            throw ActiveCartItemDeliveryException.ById(cart.Id);

        GetCustomizationsWeightByIdsQuery weightsQuery = new(
            Ids: [.. cart.Items
                    .Where(i => i.ForDelivery && i.CustomizationId is not null)
                    .Select(i => i.CustomizationId!.Value)
            ]
        );
        var weights = await sender.SendQueryAsync(weightsQuery, ct).ConfigureAwait(false);

        CalculateShipmentQuery query = new(
            ParcelCount: cart.TotalDeliveryCount,
            TotalWeight: weights.Sum(x => x.Value),
            Address: req.Address
        );
        CalculationDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return [.. calculations.Select(c => c.ToCalculateCartShipmentDto(timeZone))];
    }
}
