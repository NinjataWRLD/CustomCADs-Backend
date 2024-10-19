using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest;