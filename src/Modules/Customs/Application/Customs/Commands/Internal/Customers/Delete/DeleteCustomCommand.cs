﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Delete;

public sealed record DeleteCustomCommand(
	CustomId Id,
	AccountId BuyerId
) : ICommand;
