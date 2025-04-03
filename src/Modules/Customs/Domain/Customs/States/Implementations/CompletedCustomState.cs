using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Domain.Customs.States.Implementations;

public class CompletedCustomState : BaseCustomState
{
    public override CustomStatus Status => CustomStatus.Completed;
}
