using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.SetDelivery;

public record SetCustomDeliveryCommand(
    CustomId Id,
    bool Value,
    AccountId BuyerId
) : ICommand;
