using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;

public class CalculateOngoingOrderShipmentHandler(IOngoingOrderReads reads, IRequestSender sender)
    : IQueryHandler<CalculateOngoingOrderShipmentQuery, CalculateShipmentDto[]>
{
    public async Task<CalculateShipmentDto[]> Handle(CalculateOngoingOrderShipmentQuery req, CancellationToken ct)
    {
        OngoingOrder order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<OngoingOrder>.ById(req.Id);

        if (!order.Delivery)
            throw new CustomException("The Ongoing Order is not marked for delivery");

        GetCustomizationWeightByIdQuery customizationIdQuery = new(req.CustomizationId);
        double weight = await sender.SendQueryAsync(customizationIdQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(order.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        CalculateShipmentQuery query = new(
            ParcelCount: req.Count,
            TotalWeight: weight * req.Count,
            TimeZone: timeZone,
            Address: req.Address
        );
        CalculateShipmentDto[] calculations = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return calculations;
    }
}
