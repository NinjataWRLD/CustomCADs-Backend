using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;

public record CalculateActiveCartShipmentQuery(
    AccountId BuyerId,
    AddressDto Address
) : IQuery<CalculateShipmentDto[]>;