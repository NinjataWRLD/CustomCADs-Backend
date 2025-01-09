﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

public sealed record PurchaseOngoingOrderCommand(
    OngoingOrderId OrderId,
    string PaymentMethodId,
    AccountId BuyerId
) : ICommand<string>;
