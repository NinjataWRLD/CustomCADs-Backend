using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;

public sealed record ReportCustomCommand(
	CustomId Id,
	AccountId DesignerId
) : ICommand;
