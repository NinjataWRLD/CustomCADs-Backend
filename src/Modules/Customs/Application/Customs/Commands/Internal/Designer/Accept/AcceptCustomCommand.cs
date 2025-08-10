using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;

public sealed record AcceptCustomCommand(
	CustomId Id,
	AccountId DesignerId
) : ICommand;
