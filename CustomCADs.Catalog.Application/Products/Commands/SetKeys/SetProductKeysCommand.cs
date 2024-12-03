using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.SetKeys;

public sealed record SetProductKeysCommand(
    ProductId Id,
    AccountId CreatorId,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
