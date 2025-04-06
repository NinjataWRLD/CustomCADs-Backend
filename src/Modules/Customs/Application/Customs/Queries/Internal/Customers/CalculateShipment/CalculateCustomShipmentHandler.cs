using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

public class CalculateCustomShipmentHandler(ICustomReads reads, IRequestSender sender)
    : IQueryHandler<CalculateCustomShipmentQuery, CalculateShipmentDto[]>
{
    public async Task<CalculateShipmentDto[]> Handle(CalculateCustomShipmentQuery req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (!custom.ForDelivery)
            throw new CustomException("The Custom is not marked for delivery");

        GetCustomizationWeightByIdQuery customizationIdQuery = new(req.CustomizationId);
        double weight = await sender.SendQueryAsync(customizationIdQuery, ct).ConfigureAwait(false);

        GetTimeZoneByIdQuery timeZoneQuery = new(custom.BuyerId);
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
