using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Queries;

public record CategoryReadDto(CategoryId Id, string Name);
