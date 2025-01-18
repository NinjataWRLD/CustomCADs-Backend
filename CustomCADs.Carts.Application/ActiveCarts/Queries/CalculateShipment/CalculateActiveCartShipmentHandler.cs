using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public class CalculateActiveCartShipmentHandler(IActiveCartReads reads, IRequestSender sender)
    : IQueryHandler<CalculateActiveCartShipmentQuery, CalculateActiveCartShipmentDto[]>
{
    public async Task<CalculateActiveCartShipmentDto[]> Handle(CalculateActiveCartShipmentQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        CalculateShipmentQuery query = new(
            ParcelCount: cart.TotalDeliveryCount,
            TotalWeight: cart.TotalDeliveryWeight,
            Address: req.Address
        );
        CalculationDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return [.. calculations.Select(c => c.ToCalculateCartShipmentDto(timeZone))];
    }
}
