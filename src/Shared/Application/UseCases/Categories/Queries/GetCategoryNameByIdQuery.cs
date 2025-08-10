namespace CustomCADs.Shared.Application.UseCases.Categories.Queries;

public sealed record GetCategoryNameByIdQuery(
	CategoryId Id
) : IQuery<string>;
