using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.SetStatus;

public sealed record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status,
    AccountId DesignerId
) : ICommand;
