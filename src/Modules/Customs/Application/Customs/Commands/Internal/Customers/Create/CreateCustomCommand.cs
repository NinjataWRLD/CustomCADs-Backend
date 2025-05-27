using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;

public sealed record CreateCustomCommand(
	string Name,
	string Description,
	bool ForDelivery,
	AccountId BuyerId
) : ICommand<CustomId>;
