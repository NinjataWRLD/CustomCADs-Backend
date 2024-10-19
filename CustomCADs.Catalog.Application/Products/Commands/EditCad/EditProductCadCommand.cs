using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.EditCad;

public record EditProductCadCommand(Guid Id, EditProductCadDto Dto) : IRequest;
