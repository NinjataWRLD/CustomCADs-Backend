using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts;

using static ActiveCartsData;

public class ActiveCartsBaseUnitTests
{
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static ActiveCartItem CreateItem(
        AccountId? buyerId = null,
        ProductId? productId = null
    ) => ActiveCartItem.Create(
            buyerId: buyerId ?? ValidBuyerId,
            productId: productId ?? ValidProductId
        );

    protected static ActiveCartItem CreateItemWithDelivery(
        AccountId? buyerId = null,
        ProductId? productId = null,
        CustomizationId? customizationId = null
    ) => ActiveCartItem.Create(
            buyerId: buyerId ?? ValidBuyerId,
            productId: productId ?? ValidProductId,
            customizationId: customizationId ?? ValidCustomizationId
        );
}
