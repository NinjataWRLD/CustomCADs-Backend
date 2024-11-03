using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : ICommand;