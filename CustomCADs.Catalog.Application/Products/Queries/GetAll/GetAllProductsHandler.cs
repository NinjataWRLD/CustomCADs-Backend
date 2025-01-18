using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public sealed class GetAllProductsHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetAllProductsQuery, Result<GetAllProductsDto>>
{
    public async Task<Result<GetAllProductsDto>> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery productQuery = new(
            CategoryId: req.CategoryId,
            CreatorId: req.CreatorId,
            Status: req.Status,
            Name: req.Name,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<Product> result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] userIds = [.. result.Items.Select(p => p.CreatorId).Distinct()];
        Dictionary<AccountId, string> users = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(userIds), ct).ConfigureAwait(false);

        CategoryId[] categoryIds = [.. result.Items.Select(p => p.CategoryId).Distinct()];
        Dictionary<CategoryId, string> categories = await sender
            .SendQueryAsync(new GetCategoryNamesByIdsQuery(categoryIds), ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(c => c.CreatorId)];
        Dictionary<AccountId, string> timeZones = await sender
            .SendQueryAsync(new GetTimeZonesByIdsQuery(buyerIds), ct).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: result.Items.Select(p => p.ToGetAllProductsItem(
                username: users[p.CreatorId],
                categoryName: categories[p.CategoryId],
                timeZone: timeZones[p.CreatorId]
            )).ToArray()
        );
    }
}
