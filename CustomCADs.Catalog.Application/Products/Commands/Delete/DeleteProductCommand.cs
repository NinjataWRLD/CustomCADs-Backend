using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public record DeleteProductCommand(ProductId Id) : ICommand;