using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductCommand(Guid Id, EditProductDto Dto) : ICommand;