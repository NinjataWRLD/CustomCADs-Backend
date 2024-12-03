using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Commands.Delete;

public sealed record DeleteCartCommand(
    CartId Id,
    AccountId BuyerId
) : ICommand;
