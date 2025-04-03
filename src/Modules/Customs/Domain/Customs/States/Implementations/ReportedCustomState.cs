using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class ReportedCustomState : BaseCustomState
{
    public override CustomStatus Status => CustomStatus.Reported;

    public override void Cancel(Custom custom)
    {
        custom.ClearAcceptInfo();
        custom.SetState(new PendingCustomState());
    }
}
