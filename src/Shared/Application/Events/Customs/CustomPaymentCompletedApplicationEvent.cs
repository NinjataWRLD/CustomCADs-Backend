namespace CustomCADs.Shared.Application.Events.Customs;

public record CustomPaymentCompletedApplicationEvent(
	CustomId Id,
	AccountId BuyerId
) : BaseApplicationEvent;
