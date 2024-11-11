using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public record CreateProductCommand(CreateProductDto Dto) : ICommand<ProductId>;
