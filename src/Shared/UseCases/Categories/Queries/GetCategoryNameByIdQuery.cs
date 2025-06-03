namespace CustomCADs.Shared.UseCases.Categories.Queries;

public sealed record GetCategoryNameByIdQuery(
	CategoryId Id
) : IQuery<string>;
