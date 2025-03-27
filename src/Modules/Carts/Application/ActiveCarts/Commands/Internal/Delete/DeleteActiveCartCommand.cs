using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Delete;

public sealed record DeleteActiveCartCommand(
    AccountId BuyerId
) : ICommand;
