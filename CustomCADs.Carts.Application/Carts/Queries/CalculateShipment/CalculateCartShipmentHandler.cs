using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.Carts.Queries.CalculateShipment;

public class CalculateCartShipmentHandler(ICartReads reads, IRequestSender sender, IDeliveryService delivery)
    : IQueryHandler<CalculateCartShipmentQuery, CalculateCartShipmentDto[]>
{
    public async Task<CalculateCartShipmentDto[]> Handle(CalculateCartShipmentQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

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
