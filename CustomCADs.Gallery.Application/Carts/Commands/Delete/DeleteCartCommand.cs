using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Commands.Delete;

public record DeleteCartCommand(
    CartId Id,
    AccountId BuyerId
) : ICommand;
