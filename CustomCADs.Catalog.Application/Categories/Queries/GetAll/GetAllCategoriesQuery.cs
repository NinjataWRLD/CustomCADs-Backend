using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetAll;

public record GetAllCategoriesQuery : IQuery<IEnumerable<CategoryReadDto>>;
