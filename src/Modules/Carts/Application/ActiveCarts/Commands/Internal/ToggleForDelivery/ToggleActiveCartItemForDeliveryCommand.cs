using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryCommand(
    AccountId BuyerId,
    ProductId ProductId,
    CustomizationId? CustomizationId
) : ICommand;
