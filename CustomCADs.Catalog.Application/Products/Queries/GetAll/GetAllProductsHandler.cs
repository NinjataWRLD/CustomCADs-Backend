using CustomCADs.Account.Application.Users.Queries.GetUsersWithIds;
using CustomCADs.Catalog.Domain.Products.Reads;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public class GetAllProductsHandler(IProductReads reads, IMediator mediator)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsDto>
{
    public async Task<GetAllProductsDto> Handle(GetAllProductsQuery req, CancellationToken ct)
    {
        ProductQuery query = new(
            CreatorId: req.CreatorId,
            Status: req.Status,
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );

        ProductResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);
        Guid[] ids = result.Products.Select(p => p.CreatorId).Distinct().ToArray();

        GetUsersWithIdsQuery usersQuery = new(ids);
        IEnumerable<GetUsersWithIdsDto> users = await mediator.Send(usersQuery, ct).ConfigureAwait(false);

        GetAllProductsDto response = new(
            result.Count,
            result.Products.Select(p => 
                new GetAllProductsItem(p, users.Single(u => u.Id == p.CreatorId).Username)
            ).ToArray()
        );
        return response;
    }
}
