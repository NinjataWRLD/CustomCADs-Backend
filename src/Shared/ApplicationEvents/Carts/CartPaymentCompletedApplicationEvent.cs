using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Shared.ApplicationEvents.Carts;

public record CartPaymentCompletedApplicationEvent(
	PurchasedCartId Id,
	AccountId BuyerId
) : BaseApplicationEvent;
