using MediatR;

namespace CustomCADs.Catalog.Application.Products.Commands.EditCad;

public record SetProductCadCommand(Guid Id, SetProductCadDto Dto) : IRequest;
