using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Shared.Queries.Products;

public record GetProductPriceByIdQuery(ProductId Id) : IQuery<decimal>;