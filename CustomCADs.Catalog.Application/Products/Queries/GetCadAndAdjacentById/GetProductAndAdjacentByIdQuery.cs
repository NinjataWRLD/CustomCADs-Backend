using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdQuery(Guid Id) : IRequest<GetProductAndAdjacentByIdDto>;
