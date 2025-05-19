using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

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
