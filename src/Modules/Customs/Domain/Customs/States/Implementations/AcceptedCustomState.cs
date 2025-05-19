using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class AcceptedCustomState : BaseCustomState
{
	public override CustomStatus Status => CustomStatus.Accepted;

	public override void Begin(Custom custom)
	{
		custom.SetState(new BegunCustomState());
	}

	public override void Cancel(Custom custom)
	{
		custom.ClearAcceptInfo();
		custom.SetState(new PendingCustomState());
	}

	public override void Report(Custom custom)
	{
		custom.SetState(new ReportedCustomState());
	}
}
