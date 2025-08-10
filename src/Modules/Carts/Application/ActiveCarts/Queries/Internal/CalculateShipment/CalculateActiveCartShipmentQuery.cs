using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

public record CalculateActiveCartShipmentQuery(
	AccountId BuyerId,
	AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
