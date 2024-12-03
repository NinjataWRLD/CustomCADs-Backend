using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Inventory.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Inventory.Application.Products.Queries.GetAll;

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
            Page: req.Page,
            Limit: req.Limit
        );
        Result<Product> result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        AccountId[] userIds = [.. result.Items.Select(p => p.CreatorId).Distinct()];
        IEnumerable<(AccountId Id, string Username)> users = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(userIds), ct).ConfigureAwait(false);

        CategoryId[] categoryIds = [.. result.Items.Select(p => p.CategoryId).Distinct()];
        IEnumerable<(CategoryId Id, string Name)> categories = await sender
            .SendQueryAsync(new GetCategoriesByIdsQuery(categoryIds), ct).ConfigureAwait(false);

        AccountId[] buyerIds = [.. result.Items.Select(c => c.CreatorId)];
        GetTimeZonesByIdsQuery timeZonesQuery = new(buyerIds);
        (AccountId Id, string TimeZone)[] timeZones = await sender
            .SendQueryAsync(timeZonesQuery, ct).ConfigureAwait(false);

        return new(
            Count: result.Count,
            Items: result.Items.Select(p => p.ToGetAllProductsItem(
                username: users.Single(u => u.Id == p.CreatorId).Username,
                categoryName: categories.Single(c => c.Id == p.CategoryId).Name,
                timeZone: timeZones.Single(t => t.Id == p.CreatorId).TimeZone
            )).ToArray()
        );
    }
}
