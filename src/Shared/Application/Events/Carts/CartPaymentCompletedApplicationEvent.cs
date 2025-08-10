namespace CustomCADs.Shared.Application.Events.Carts;

public record CartPaymentCompletedApplicationEvent(
	PurchasedCartId Id,
	AccountId BuyerId
) : BaseApplicationEvent;
