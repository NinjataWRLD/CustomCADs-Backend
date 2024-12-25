using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Application.Carts.Queries.CalculateShipment;

public record CalculateCartShipmentQuery(
    CartId Id,
    AddressDto Address
) : IQuery<CalculateCartShipmentDto[]>;