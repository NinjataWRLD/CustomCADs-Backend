using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Catalog.Application.Categories.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Queries.Users.GetUsernamesByIds;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads, IRequestSender sender)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsDto>
{
    public async Task<GetAllProductsDto> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery productQuery = new(
            CreatorId: req.CreatorId,
            Status: req.Status,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        ProductResult result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        UserId[] userIds = result.Products.Select(p => p.CreatorId).Distinct().ToArray();
        IEnumerable<(UserId Id, string Username)> users = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(userIds), ct)
            .ConfigureAwait(false);

        CategoryId[] categoryIds = result.Products.Select(p => p.CategoryId).Distinct().ToArray();
        IEnumerable<CategoryReadDto> categories = await sender
            .SendQueryAsync(new GetAllCategoriesQuery(), ct)
            .ConfigureAwait(false);


        GetAllProductsDto response = new(
            result.Count,
            result.Products.Select(product =>
                new GetAllProductsItem(
                    product, 
                    users.Single(u => u.Id == product.CreatorId).Username, 
                    categories.Single(u => u.Id == product.CategoryId).Name
            )).ToArray()
        );
        return response;
    }
}
