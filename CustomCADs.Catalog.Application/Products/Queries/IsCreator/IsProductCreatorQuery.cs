using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Queries.IsCreator;

public record IsProductCreatorQuery(Guid Id, Guid CreatorId) : IQuery<bool>;
