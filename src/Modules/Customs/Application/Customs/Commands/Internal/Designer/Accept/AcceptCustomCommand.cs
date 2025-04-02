using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;

public sealed record AcceptCustomCommand(
    CustomId Id,
    AccountId DesignerId
) : ICommand;
