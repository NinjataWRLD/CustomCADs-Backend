using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.Domain.Customs.States;

public interface ICustomState
{
	CustomStatus Status { get; }
	void Accept(Custom custom, AccountId designerId);
	void Begin(Custom custom);
	void Finish(Custom custom, CadId cadId, decimal price);
	void Complete(Custom custom, CustomizationId? customizationId);
	void Cancel(Custom custom);
	void Report(Custom custom);
}
