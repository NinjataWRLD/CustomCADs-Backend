using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetAll;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryReadDto>>;
