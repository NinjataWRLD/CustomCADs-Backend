using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(Guid Id) : IRequest<bool>;