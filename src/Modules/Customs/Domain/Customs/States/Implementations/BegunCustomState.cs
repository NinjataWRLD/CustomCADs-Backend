using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class BegunCustomState : BaseCustomState
{
    public override CustomStatus Status => CustomStatus.Begun;

    public override void Cancel(Custom custom)
    {
        custom.ClearAcceptInfo();
        custom.SetState(new PendingCustomState());
    }

    public override void Finish(Custom custom, CadId cadId, decimal price)
    {
        custom.FillFinishInfo(cadId, price);
        custom.SetState(new FinishedCustomState());
    }

    public override void Report(Custom custom)
    {
        custom.SetState(new ReportedCustomState());
    }
}
