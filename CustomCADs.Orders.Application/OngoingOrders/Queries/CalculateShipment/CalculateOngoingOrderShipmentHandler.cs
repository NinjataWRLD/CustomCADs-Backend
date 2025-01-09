using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;

public class CalculateOngoingOrderShipmentHandler(IOngoingOrderReads reads, IRequestSender sender, IDeliveryService delivery)
    : IQueryHandler<CalculateOngoingOrderShipmentQuery, CalculateOngoingOrderShipmentDto[]>
{
    public async Task<CalculateOngoingOrderShipmentDto[]> Handle(CalculateOngoingOrderShipmentQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OngoingOrderNotFoundException.ById(req.Id);

        CalculationDto[] calculations = await delivery.CalculateAsync(new(
            ParcelCount: 1,
            TotalWeight: req.TotalWeight,
            Country: req.Address.Country,
            City: req.Address.City
        ), ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return [.. calculations.Select(c => c.ToCalculateOrderShipmentDto(timeZone))];
    }
}
