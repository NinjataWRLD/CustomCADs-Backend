using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductCommand(ProductId Id, EditProductDto Dto) : ICommand;