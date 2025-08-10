using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

namespace CustomCADs.Customs.Domain.Customs.States;

public abstract class BaseCustomState : ICustomState
{
	public abstract CustomStatus Status { get; }

	public virtual void Accept(Custom custom, AccountId designerId)
		=> throw InvalidTransition(nameof(Accept));
	public virtual void Begin(Custom custom)
		=> throw InvalidTransition(nameof(Begin));
	public virtual void Finish(Custom custom, CadId cadId, decimal price)
		=> throw InvalidTransition(nameof(Finish));
	public virtual void Complete(Custom custom, CustomizationId? customizationId)
		=> throw InvalidTransition(nameof(Complete));
	public virtual void Cancel(Custom custom)
		=> throw InvalidTransition(nameof(Cancel));
	public virtual void Report(Custom custom)
		=> throw InvalidTransition(nameof(Report));

	protected InvalidOperationException InvalidTransition(string action)
		=> new($"Cannot {action} a Custom in the {Status} state.");
}
