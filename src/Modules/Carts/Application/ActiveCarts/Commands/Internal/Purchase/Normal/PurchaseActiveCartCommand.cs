﻿using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

public sealed record PurchaseActiveCartCommand(
	string PaymentMethodId,
	AccountId BuyerId
) : ICommand<PaymentDto>;
