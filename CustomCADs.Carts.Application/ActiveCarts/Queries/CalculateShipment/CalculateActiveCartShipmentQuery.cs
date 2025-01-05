using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public record CalculateActiveCartShipmentQuery(
    ActiveCartId Id,
    AddressDto Address
) : IQuery<CalculateActiveCartShipmentDto[]>;