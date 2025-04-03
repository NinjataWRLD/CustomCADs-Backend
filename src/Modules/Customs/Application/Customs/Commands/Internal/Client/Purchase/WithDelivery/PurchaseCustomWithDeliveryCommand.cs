using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery;

public sealed record PurchaseCustomWithDeliveryCommand(
    CustomId Id,
    string PaymentMethodId,
    string ShipmentService,
    int Count,
    AddressDto Address,
    ContactDto Contact,
    CustomizationId CustomizationId,
    AccountId BuyerId
) : ICommand<string>;
