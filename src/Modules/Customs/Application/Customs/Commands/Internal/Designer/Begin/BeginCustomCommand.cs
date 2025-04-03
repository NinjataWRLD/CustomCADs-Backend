using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Begin;

public sealed record BeginCustomCommand(
    CustomId Id,
    AccountId DesignerId
) : ICommand;
