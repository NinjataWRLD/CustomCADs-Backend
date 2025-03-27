using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Shipments.Queries;

public record CalculateShipmentQuery(
    int ParcelCount,
    double TotalWeight,
    string TimeZone,
    AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
