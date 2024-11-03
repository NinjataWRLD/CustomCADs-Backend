using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public record CreateProductCommand(CreateProductDto Dto) : ICommand<Guid>;
