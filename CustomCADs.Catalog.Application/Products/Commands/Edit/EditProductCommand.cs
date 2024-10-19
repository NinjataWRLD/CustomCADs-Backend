using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductCommand(Guid Id, EditProductDto Dto) : IRequest;