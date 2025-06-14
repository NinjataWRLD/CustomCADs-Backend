using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Shared.ApplicationEvents.Customs;

public record CustomPaymentCompletedApplicationEvent(
	CustomId Id,
	AccountId BuyerId
) : BaseApplicationEvent;
