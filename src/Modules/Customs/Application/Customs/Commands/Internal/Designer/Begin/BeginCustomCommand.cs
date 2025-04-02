using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Begin;

public sealed record BeginCustomCommand(
    CustomId Id,
    AccountId DesignerId
) : ICommand;
