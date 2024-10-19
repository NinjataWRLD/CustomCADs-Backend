using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public record CreateProductCommand(CreateProductDto Dto) : IRequest<Guid>;
