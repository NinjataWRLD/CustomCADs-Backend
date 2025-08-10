using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;

public record SetCustomDeliveryCommand(
	CustomId Id,
	bool Value,
	AccountId BuyerId
) : ICommand;
