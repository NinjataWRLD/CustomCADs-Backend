using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
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
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        ProductResult result = await reads.AllAsync(productQuery, track: false, ct: ct).ConfigureAwait(false);

        UserId[] ids = result.Products.Select(p => p.CreatorId).Distinct().ToArray();
        IEnumerable<(UserId Id, string Username)> users = await sender
            .SendQueryAsync(new GetUsernamesByIdsQuery(ids), ct)
            .ConfigureAwait(false);

        GetAllProductsDto response = new(
            result.Count,
            result.Products.Select(p =>
                new GetAllProductsItem(p, users.Single(u => u.Id == p.CreatorId).Username)
            ).ToArray()
        );
        return response;
    }
}
