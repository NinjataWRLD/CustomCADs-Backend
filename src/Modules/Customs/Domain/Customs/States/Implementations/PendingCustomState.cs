using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class PendingCustomState : BaseCustomState
{
	public override CustomStatus Status => CustomStatus.Pending;

	public override void Accept(Custom custom, AccountId designerId)
	{
		custom.FillAcceptInfo(designerId);
		custom.SetState(new AcceptedCustomState());
	}

	public override void Report(Custom custom)
	{
		custom.SetState(new ReportedCustomState());
	}
}
