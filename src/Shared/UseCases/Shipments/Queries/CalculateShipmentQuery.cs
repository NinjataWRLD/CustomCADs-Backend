using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Shared.UseCases.Shipments.Queries;

public record CalculateShipmentQuery(
    double[] Weights,
    AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
