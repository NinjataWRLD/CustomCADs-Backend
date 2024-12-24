using CustomCADs.Orders.Application.Common.Exceptions;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;

public class CalculateOrderShipmentHandler(IOrderReads reads, IRequestSender sender, IDeliveryService delivery)
    : IQueryHandler<CalculateOrderShipmentQuery, CalculateOrderShipmentDto[]>
{
    public async Task<CalculateOrderShipmentDto[]> Handle(CalculateOrderShipmentQuery req, CancellationToken ct)
    {
        Order order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw OrderNotFoundException.ById(req.Id);

        int count = 1;
        CalculationDto[] calculations = await delivery.CalculateAsync(new(
            ParcelCount: count,
            TotalWeight: req.TotalWeight,
            Country: req.Country,
            City: req.City
        ), ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return [.. calculations.Select(c => c.ToCalculateOrderShipmentDto(timeZone))];
    }
}
