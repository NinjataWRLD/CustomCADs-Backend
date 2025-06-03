using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;

public sealed record EditCustomCommand(
	CustomId Id,
	string Name,
	string Description,
	AccountId BuyerId
) : ICommand;
