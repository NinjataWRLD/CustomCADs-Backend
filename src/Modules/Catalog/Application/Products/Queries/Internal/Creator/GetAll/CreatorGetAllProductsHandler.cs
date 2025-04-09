using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

public sealed class CreatorGetAllProductsHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<CreatorGetAllProductsQuery, Result<CreatorGetAllProductsDto>>
{
    public async Task<Result<CreatorGetAllProductsDto>> Handle(CreatorGetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery productQuery = new(
            CategoryId: req.CategoryId,
            CreatorId: req.CreatorId,
            TagIds: req.TagIds,
            Name: req.Name,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<Product> result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        CategoryId[] categoryIds = [.. result.Items.Select(p => p.CategoryId).Distinct()];
        Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
            new GetCategoryNamesByIdsQuery(categoryIds),
            ct
        ).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: [.. result.Items.Select(p => p.ToCreatorGetAllDto(
                categoryName: categories[p.CategoryId]
            ))]
        );
    }
}
