using MediatR;

namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public record DeleteCategoryCommand(int Id) : IRequest;
