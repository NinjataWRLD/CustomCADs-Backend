using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public record CategoryExistsByIdQuery(int Id) : IRequest<bool>;
