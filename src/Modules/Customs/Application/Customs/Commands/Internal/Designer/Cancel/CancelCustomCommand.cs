using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

public sealed record CancelCustomCommand(
    CustomId Id,
    AccountId DesignerId
) : ICommand;
