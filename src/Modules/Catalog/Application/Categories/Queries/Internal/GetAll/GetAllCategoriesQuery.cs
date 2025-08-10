namespace CustomCADs.Catalog.Application.Categories.Queries.Internal.GetAll;

public sealed record GetAllCategoriesQuery
	: IQuery<IEnumerable<CategoryReadDto>>;
