using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdDto>;
