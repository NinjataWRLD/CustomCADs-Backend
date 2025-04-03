using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;

public sealed record ReportCustomCommand(
    CustomId Id,
    AccountId DesignerId
) : ICommand;
