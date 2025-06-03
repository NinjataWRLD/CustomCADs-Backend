using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;

public sealed record FinishCustomCommand(
	CustomId Id,
	(string Key, string ContentType, decimal Volume) Cad,
	decimal Price,
	AccountId DesignerId
) : ICommand;
