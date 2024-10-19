using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdDto>;