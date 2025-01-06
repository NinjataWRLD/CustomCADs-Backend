using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public class CalculateActiveCartShipmentHandler(IActiveCartReads reads, IRequestSender sender, IDeliveryService delivery)
    : IQueryHandler<CalculateActiveCartShipmentQuery, CalculateActiveCartShipmentDto[]>
{
    public async Task<CalculateActiveCartShipmentDto[]> Handle(CalculateActiveCartShipmentQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        CalculationDto[] calculations = await delivery.CalculateAsync(new(
            ParcelCount: cart.TotalDeliveryCount,
            TotalWeight: cart.TotalDeliveryWeight,
            Country: req.Address.Country,
            City: req.Address.City
        ), ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return [.. calculations.Select(c => c.ToCalculateCartShipmentDto(timeZone))];
    }
}
