using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class FinishedCustomState : BaseCustomState
{
	public override CustomStatus Status => CustomStatus.Finished;

	public override void Complete(Custom custom, CustomizationId? customizationId)
	{
		custom.FillCompleteInfo(customizationId);
		custom.SetState(new CompletedCustomState());
	}
}
