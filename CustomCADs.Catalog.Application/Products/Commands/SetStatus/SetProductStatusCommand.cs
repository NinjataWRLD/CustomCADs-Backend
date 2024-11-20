using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(
    ProductId Id,
    ProductStatus Status
) : ICommand;
