using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Shipments.Queries;

public record CalculateShipmentQuery(
    int ParcelCount,
    double TotalWeight,
    AddressDto Address
) : IQuery<CalculationDto[]>;
