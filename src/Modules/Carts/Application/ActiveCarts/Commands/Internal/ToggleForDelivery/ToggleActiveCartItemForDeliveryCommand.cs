using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryCommand(
	AccountId BuyerId,
	ProductId ProductId,
	CustomizationId? CustomizationId
) : ICommand;
