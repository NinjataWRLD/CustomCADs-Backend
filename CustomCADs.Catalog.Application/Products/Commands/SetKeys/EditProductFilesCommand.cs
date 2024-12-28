using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.SetKeys;

public sealed record EditProductFilesCommand(
    ProductId Id,
    (string? Key, string? ContentType) Cad,
    (string? Key, string? ContentType) Image,
    AccountId CreatorId
) : ICommand;
