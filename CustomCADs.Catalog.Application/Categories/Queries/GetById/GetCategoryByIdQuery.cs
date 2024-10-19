using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryReadDto>;
