using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public record CreateCategoryCommand(CategoryWriteDto Dto) : IRequest<int>;
